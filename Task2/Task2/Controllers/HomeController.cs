using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Task2.ViewModels;
using Task2.Helpers;
using Task2.Models.FeedModels;
using System.Collections.Generic;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        /// <summary>
        /// Отображение главной страницы приложения
        /// </summary>
        /// <returns></returns>
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
        public IActionResult UpdateSettings(string[] feedLink, int updateTime)
        {
            // попытка обновления файла настроек
            string updateResult = Configurator.EditSettings(feedLink.ToList(), updateTime);

            if (updateResult == "ok")
                TempData["Success"] = "Настройки успешно обновлены";
            else
                TempData["Error"] = updateResult;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Вывод частичного представления со статьями выбранной RSS-ленты
        /// </summary>
        /// <param name="feed">Выбранная лента</param>
        public IActionResult ChosenFeed(int feed)
        {
            string feedUrl;
            if ((feed < Configurator.Settings.Feeds.Count) && (feed >= 0))
                // ссылка на ленту по её номеру
                feedUrl = Configurator.Settings.Feeds[feed];
            else
                // некорректный номер либо не выбрано ничего
                return PartialView(null);

            // получить указанную ленту
            Rss rss = Connector.GetRSSFeed(feedUrl);
            if (rss == null)
                return PartialView(null);
            else
                return PartialView(rss.Channel);
        }
    }
}
