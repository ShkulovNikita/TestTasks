using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Task2.Models
{
    /// <summary>
    /// Настройки веб-приложения
    /// </summary>
    public class Settings
    {
        public Settings() { }

        /// <summary>
        /// Создание экземпляра данного класса 
        /// на основе данных из XML-файла настроек
        /// </summary>
        /// <param name="settings"></param>
        public Settings(Config settings)
        {
            Feeds = settings.Feeds;
            UpdateTime = settings.Update;
        }

        /// <summary>
        /// Список лент
        /// </summary>
        [Required (ErrorMessage = "Не указаны адреса лент")]
        [Remote(action: "ValidateUrls", controller: "Home", ErrorMessage = "Введен некорректный адрес RSS-ленты")]
        public List<string> Feeds { get; set; } = new List<string>();

        /// <summary>
        /// Частота обновления
        /// </summary>
        [Required (ErrorMessage = "Не указана частота обновления")]
        [Range(1, int.MaxValue/1000, ErrorMessage = "Период должен быть больше нуля")]
        public int UpdateTime { get; set; }
    }
}
