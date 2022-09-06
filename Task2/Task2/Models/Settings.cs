using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public List<string> Feeds { get; set; } = new List<string>();

        /// <summary>
        /// Частота обновления
        /// </summary>
        [Required (ErrorMessage = "Не указана частота обновления")]
        [Range(1, int.MaxValue, ErrorMessage = "Период должен быть больше нуля")]
        public int UpdateTime { get; set; }
    }
}
