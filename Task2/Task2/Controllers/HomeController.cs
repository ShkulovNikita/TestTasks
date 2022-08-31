using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Task2.ViewModels;
using Task2.Helpers;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index()
        {
            //попытка получить настройки из файла
            Configurator.UpdateSettings();
            if (Configurator.Settings == null)
            {
                TempData["Error"] = "Не удалось получить настройки из файла";
                return View();
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
        public IActionResult Index(string[] feedLink, int updateTime)
        {
            // попытка обновления файла настроек
            string updateResult = Configurator.EditSettings(feedLink.ToList(), updateTime);

            if (updateResult == "ok")
                TempData["Success"] = "Настройки успешно обновлены";
            else
                TempData["Error"] = updateResult;

            // создание обновленной ViewModel
            FeedViewModel feedVm = new FeedViewModel(Configurator.Settings);

            return View(feedVm);
        }
    }
}
