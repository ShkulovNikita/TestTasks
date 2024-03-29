﻿using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace Task2.Models.FeedModels
{
    /// <summary>
    /// RSS-канал
    /// </summary>
    public class Channel
    {
        public Channel () { }

        /// <summary>
        /// Название RSS-канала
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ссылка на источник
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Статьи канала
        /// </summary>
        [XmlElement("Item")]
        public List<Item> Items { get; set; } = new List<Item>();

        /// <summary>
        /// Внутренний идентификатор канала
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Сформировать внутренние идентификаторы статей
        /// </summary>
        public void CreateItemIds ()
        {
            foreach (Item item in Items)
            {
                item.CreateId(Title);
            }
        }

        /// <summary>
        /// Получение списка идентификаторов статей канала
        /// </summary>
        /// <returns>Список с идентификаторами статей</returns>
        public List<string> GetItemsIds()
        {
            if (Items.Count > 0)
                return Items.Select(i => i.ArticleId).ToList();
            else
                return new List<string>();
        }

        /// <summary>
        /// Ссылка на ленту, используемая при подключении
        /// </summary>
        public string ConnectionLink { get; set; }

        /// <summary>
        /// Создать внутренний идентификатор канала
        /// </summary>
        public void CreateId ()
        {
            ChannelId = string.Concat(Title).GetHashCode().ToString();
        }
    }
}
