using System.Collections.Generic;
using Task2.Models;
using Task2.Models.FeedModels;

namespace Task2.ViewModels
{
    /// <summary>
    /// Модель представления для страницы веб-приложения
    /// </summary>
    public class FeedViewModel
    {
        public FeedViewModel() { }

        public FeedViewModel(Config settings)
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
        public int UpdateTime { get; set; }
    }
}
