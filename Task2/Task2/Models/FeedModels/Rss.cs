namespace Task2.Models.FeedModels
{
    /// <summary>
    /// Класс, соответствующий корневому тегу XML-ленты RSS
    /// </summary>
    public class Rss
    {
        public Rss () { }

        /// <summary>
        /// RSS-канал с лентой
        /// </summary>
        public Channel Channel { get; set; }
    }
}
