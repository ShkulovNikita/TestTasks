using System.Collections.Generic;
using System.Xml.Serialization;

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
        public List<Item> Items { get; set; }
    }
}
