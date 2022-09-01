using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

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
        static public string GetResponse(string url)
        {
            // объект клиента для выполнения запроса
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

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


    }
}
