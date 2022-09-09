using System;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
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
            HttpClientHandler handler = CreateHandler();

            if (handler == null)
            {
                return "Произошла ошибка\r\n";
            }

            HttpClient client = new HttpClient(handler: handler, disposeHandler: true);

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

            try
            {
                //получение ответа от API
                HttpResponseMessage response = client.GetAsync("").Result;

                //ответ успешно получен
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsStringAsync().Result;
                //произошла ошибка
                else
                    return "Произошла ошибка\r\n" + response.StatusCode.ToString() + ": " + response.ReasonPhrase;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "Произошла ошибка\r\n" + ex.Message;
            }
            
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

            // если есть лента со статьими, то сформировать для них идентификаторы
            if (feed != null)
                if (feed.Channel != null)
                {
                    feed.Channel.CreateId();
                    feed.Channel.CreateItemIds();
                }
                    
            return feed;
        }

        /// <summary>
        /// Получение указанных RSS-лент
        /// </summary>
        /// <param name="feedUrls">Ссылки на ленты</param>
        /// <returns>Список с указанными лентами</returns>
        static public List<Channel> GetRssChannels(List<string> feedUrls)
        {
            List<Channel> feeds = new List<Channel>();
            foreach (string feedUrl in feedUrls)
            {
                // получение очередной ленты
                Rss rss = GetRSSFeed(feedUrl);

                // проверить, что объект RSS был действительно получен
                if (rss != null)
                {
                    // проверить наличие в нем канала с лентой
                    if (rss.Channel != null)
                        // добавить данный канал в общий список
                        feeds.Add(rss.Channel);
                    else
                        // если ничего не получено,
                        // то создать "пустой" канал для указанной ссылки
                        feeds.Add(new Channel { ConnectionLink = feedUrl });
                }
                // если не был получен - создать пустой объект,
                // чтобы представление "знало" о нем
                else
                    feeds.Add(new Channel { ConnectionLink = feedUrl });
            }

            return feeds;
        }
    }
}
