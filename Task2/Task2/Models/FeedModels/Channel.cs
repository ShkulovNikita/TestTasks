using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace Task2.Models.FeedModels
{
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
        /// Описание канала
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Статьи канала
        /// </summary>
        [XmlElement("Item")]
        public List<Item> Items { get; set; } = new List<Item>();

        /// <summary>
        /// Сформировать внутренние идентификаторы статей
        /// </summary>
        public void CreateItemIds ()
        {
            foreach (Item item in Items)
            {
                item.CreateId();
            }
        }

        /// <summary>
        /// Получение списка идентификаторов статей канала
        /// </summary>
        /// <returns>Список с идентификаторами статей</returns>
        public List<string> GetItemsIds()
        {
            List<string> result = Items.Select(i => i.ArticleId).ToList();
            return result;
        }
    }
}
