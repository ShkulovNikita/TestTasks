using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Task2.ViewModels;
using Task2.Helpers;
using Task2.Models.FeedModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        /// <summary>
        /// Отображение главной страницы приложения
        /// </summary>
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
            FeedViewModel feedVm = new FeedViewModel(Configurator.Settings);

            return View(feedVm);
        }

        /// <summary>
        /// Обновление настроек
        /// </summary>
        /// <param name="feedLink">Список выбранных лент</param>
        /// <param name="updateTime">Частота обновления</param>
        [HttpPost]
        public IActionResult UpdateSettings(string[] feedLink, int updateTime, bool format)
        {
            // попытка обновления файла настроек
            string updateResult = Configurator.EditSettings(feedLink.ToList(), updateTime);

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

            return PartialView(feeds);
        }
    }
}
