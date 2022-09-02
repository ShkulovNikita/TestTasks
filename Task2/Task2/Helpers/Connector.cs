using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Task2.Models.FeedModels;

namespace Task2.Helpers
{
    /// <summary>
    /// Класс с методами для подключения к RSS-лентам 
    /// и получения из них данных
    /// </summary>
    static public class Connector
    {
        /// <summary>
        /// Получение ответа от RSS
        /// </summary>
        /// <param name="url">URL-адрес ленты</param>
        /// <returns>RSS-лента в формате XML</returns>
        static private string GetResponse(string url)
        {
            // объект клиента для выполнения запроса
            HttpClient client = new HttpClient();

            // проверка Url
            try
            {
                client.BaseAddress = new Uri(url);
            }
            catch (Exception ex)
            {
                return "Произошла ошибка\r\n" + ex.Message;
            }

            //добавление заголовка с указанием принимаемого формата - XML
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/xml"));

            //получение ответа от API
            HttpResponseMessage response = client.GetAsync("").Result;

            //ответ успешно получен
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            //произошла ошибка
            else
                return "Произошла ошибка\r\n" + response.StatusCode.ToString() + ": " + response.ReasonPhrase;
        }

        /// <summary>
        /// Получение указанной RSS-ленты
        /// </summary>
        /// <param name="feedUrl">Адрес RSS-ленты</param>
        /// <returns>Объект ленты с её статьями</returns>
        static public Rss GetRSSFeed(string feedUrl)
        {
            // получить XML ленты от источника
            string feedXml = GetResponse(feedUrl);

            if (feedXml.Contains("Произошла ошибка"))
                return null;

            // получить объект с лентой
            Rss feed = Parser.ParseRss(feedXml);

            return feed;
        }

        /// <summary>
        /// Получение указанных RSS-лент
        /// </summary>
        /// <param name="feedUrls">Ссылки на ленты</param>
        /// <returns>Список с указанными лентами</returns>
        static public List<Rss> GetRSSFeeds(List<string> feedUrls)
        {
            List<Rss> feeds = new List<Rss>();
            foreach (string feedUrl in feedUrls)
                feeds.Add(GetRSSFeed(feedUrl));

            return feeds;
        }
    }
}
