using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Task2.Helpers;
using Task2.Models.FeedModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Task2.Models;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        /// <summary>
        /// Отображение главной страницы приложения
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            //попытка получить настройки из файла
            Configurator.UpdateSettings();
            if (Configurator.Settings == null)
            {
                TempData["Error"] = "Не удалось получить настройки из файла";
                return View(null);
            }

            // проверка подключения к прокси-серверу посредством
            // тестового запроса к некоторой RSS-ленте
            string proxyResult = Connector.TestProxyConnection("https://habr.com/rss/interesting/");
            if (proxyResult != "ok")
            {
                TempData["Error"] = proxyResult;
                return View(null);
            }

            // создание ViewModel
            Settings feedVm = new Settings(Configurator.Settings);

            return View(feedVm);
        }

        /// <summary>
        /// Обновление настроек
        /// </summary>
        /// <param name="settings">Настройки, выбранные пользователем</param>
        /// <param name="format">false - выводить описание без форматирования тегов, 
        /// true - с форматированием</param>
        [HttpPost]
        public IActionResult UpdateSettings(Settings settings, bool format)
        {
            // валидация настроек
            string validationErrors = "";

            // частота обновления должна быть положительной
            if (settings.UpdateTime <= 0)
                validationErrors += "Частота обновления должна быть положительным числом.\r\n";
            // проверка слишком большого значения
            if (settings.UpdateTime > int.MaxValue)
                validationErrors += "Задано слишком большое число.\n";
            // отсутствие списка лент
            if (settings.Feeds == null)
                validationErrors += "Не заданы RSS-ленты.\n";
            // если список лент существует - проверка его содержимого
            else
            {
                // пустота списка лент
                if (settings.Feeds.Count == 0)
                    validationErrors += "Не заданы RSS-ленты.\n";
                // присутствие пустых адресов лент
                if (!FeedsValidator.CheckEmptiness(settings.Feeds))
                    validationErrors += "Адрес ленты не может быть пустым.\n";
                // присутствие некорректных адресов лент
                if (!FeedsValidator.CheckUrls(settings.Feeds))
                    validationErrors += "Задан некорректный адрес ленты.\n";
                // повторения значений
                if (!FeedsValidator.CheckRepeatings(settings.Feeds))
                    validationErrors += "Адреса лент не должны повторяться.\n";
            }
            // были ли обнаружены ошибки при проверках выше
            if (validationErrors != "")
            {
                TempData["ValidationError"] = validationErrors;
                return RedirectToAction("Index");
            }
                
            // попытка обновления файла настроек
            string updateResult = Configurator.EditSettings(settings.Feeds.ToList(), settings.UpdateTime);

            if (updateResult == "ok")
                TempData["Success"] = "Настройки успешно обновлены";
            else
                TempData["Error"] = updateResult;

            // сохранить форматирование описания в сессию
            HttpContext.Session.SetString("format", format.ToString());

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Вывод частичного представления с выбранными для отображения RSS-лентами
        /// </summary>
        public IActionResult RssFeeds()
        {
            // не выбрано ни одной ленты
            if ((Configurator.Settings.Feeds.Count == 0))
            {
                TempData["MessagePartial"] = "Не выбраны ленты для отображения";
                return null;
            }
            // единственная лента - пустая
            if (Configurator.Settings.Feeds[0] == "")
            {
                TempData["MessagePartial"] = "Не выбраны ленты для отображения";
                return null;
            }

            // получить ленты
            List<Channel> feeds = Connector.GetRssChannels(Configurator.Settings.Feeds);

            // чистка в сессии идентификаторов тех статей, которых больше нет
            ClearUnusedIds(feeds);

            return PartialView(feeds);
        }

        /// <summary>
        /// Сохранить в сессии, описания каких статей уже были открыты,
        /// чтобы после обновления лент они оставались открытыми для пользователя
        /// </summary>
        /// <param name="id">Идентификатор статьи</param>
        /// <param name="isOpened">false: элемент теперь закрыт, true: элемент открыт</param>
        [HttpPost]
        public void SaveDescriptionState(string id, bool isOpened)
        {
            // список уже открытых описаний
            List<string> descrIds = GetOpenedDescriptions();

            // есть ли уже данная статья в списке
            if (descrIds.Contains(id))
            {
                // если статья была закрыта, то удалить из списка
                if (!isOpened)
                    descrIds.Remove(id);
            }
            // статьи в списке нет
            else
            {
                // если статья была открыта, то добавить в список
                if (isOpened)
                    descrIds.Add(id);
            }

            // записать в сессию новый список
            SessionHelper.SetObjectAsJson(HttpContext.Session, "descr_ids", descrIds);
        }

        /// <summary>
        /// Получить из сессии список тех описаний, которые уже были раскрыты пользователем
        /// </summary>
        /// <returns>Список идентификаторов описаний, которые были раскрыты пользователем</returns>
        private List<string> GetOpenedDescriptions()
        {
            // попытаться получить список из сессии
            List<string> descrIds = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, "descr_ids");

            if (descrIds != null)
                return descrIds;
            else
                return new List<string>();
        }

        /// <summary>
        /// Очистить в сессии идентификаторы тех статей, которые
        /// больше не отображаются в ленте после очередного обновления
        /// </summary>
        /// <param name="feeds">Новые ленты, полученные после обновления</param>
        private void ClearUnusedIds(List<Channel> feeds)
        {
            // список идентификаторов из сессии
            List<string> ids = GetOpenedDescriptions();

            // список идентификаторов всех статей из всех лент
            List<string> articlesIds = GetAllArticlesIds(feeds);

            // удалить из списка ids те статьи, которых нет в списке articlesIds
            int resultNumber = ids.RemoveAll(id => !articlesIds.Contains(id));

            // если произошли изменения - сохранить в сессию новый список
            if (resultNumber > 0)
                SessionHelper.SetObjectAsJson(HttpContext.Session, "descr_ids", ids);
        }

        /// <summary>
        /// Получить из лент все идентификаторы статей
        /// </summary>
        /// <param name="feeds">RSS-ленты со статьями</param>
        /// <returns>Список всех идентификаторов статей</returns>
        private List<string> GetAllArticlesIds(List<Channel> feeds)
        {
            List<string> result = new List<string>();

            foreach (Channel feed in feeds)
                result.AddRange(feed.GetItemsIds());

            return result;
        }
    }
}
