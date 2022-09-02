﻿namespace Task2.Models.FeedModels
{
    public class Item
    {
        public Item () { }

        /// <summary>
        /// Заголовок статьи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ссылка на статью
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Описание/начало статьи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата публикации
        /// </summary>
        public string PubDate { get; set; }
    }
}