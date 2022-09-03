using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Task2.Models.FeedModels;
using System.Net;
using System.Diagnostics;

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
            HttpClientHandler handler = CreateHandler();

            if (handler == null)
            {
                return "Произошла ошибка\r\n";
            }

            HttpClient client = new HttpClient(handler: handler, disposeHandler: true);

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
        /// Создать объект обработчика для выполнения запроса к RSS
        /// </summary>
        /// <returns></returns>
        static private HttpClientHandler CreateHandler()
        {
            // создание объекта, содержащего данные для подключения к прокси-серверу
            WebProxy proxy = GetProxy();

            if (proxy != null)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxy,
                };

                return httpClientHandler;
            }
            else
                return null;
        }

        /// <summary>
        /// Получение данных, необходимых для подключения к прокси-серверу
        /// </summary>
        /// <returns></returns>
        static private WebProxy GetProxy()
        {
            try
            {
                var proxy = new WebProxy
                {
                    Address = new Uri(Configurator.Settings.Proxy),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,

                    Credentials = new NetworkCredential(
                    userName: Configurator.Settings.Login,
                    password: Configurator.Settings.Password)
                };

                return proxy;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Проверить возможность подключения к лентам через прокси-сервер
        /// </summary>
        /// <param name="testUrl">Ссылка на некоторую ленту, являющаяся заведомо правильной</param>
        /// <returns>ok либо полученная ошибка</returns>
        static public string TestProxyConnection(string testUrl)
        {
            string response = GetResponse(testUrl);

            if (response.Contains("Произошла ошибка"))
                return "Невозможно подключиться к прокси-серверу. Проверьте адрес сервера и учетные данные для подключения";
            else
                return "ok";
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

            // если есть статьи, то сформировать для них идентификаторы
            if (feed != null)
                if (feed.Channel != null)
                    feed.Channel.CreateItemIds();

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
