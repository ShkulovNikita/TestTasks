using System;
using System.IO;
using System.Xml.Serialization;
using Task2.Models;
using Task2.Models.FeedModels;
using System.Diagnostics;

namespace Task2.Helpers
{
    /// <summary>
    /// Вспомогательный класс с методами для парсинга XML
    /// </summary>
    static public class Parser
    {
        /// <summary>
        /// Парсинг XML из файла настроек
        /// </summary>
        /// <param name="configName">Имя файла настроек</param>
        /// <returns>Объект с настройками приложения</returns>
        static public Config ParseSettings(string configName)
        {
            Config settings;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
            try
            {
                using (FileStream fs = new FileStream(configName, FileMode.OpenOrCreate))
                {
                    settings = xmlSerializer.Deserialize(fs) as Config;
                }

                return settings;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Запись объекта настроек в виде XML-файла
        /// </summary>
        /// <param name="configName">Имя файла настроек</param>
        /// <param name="settings">Объект с настройками приложения</param>
        /// <returns>true - успешно, false - произошла ошибка</returns>
        static public bool WriteSettings(string configName, Config settings)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));

            try
            {
                using (FileStream fs = new FileStream(configName, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, settings);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Парсинг XML из RSS-ленты
        /// </summary>
        /// <param name="feedXML">XML ленты в формате строки</param>
        /// <returns>Объект, содержащий одну ленту</returns>
        static public Rss ParseRss(string feedXML)
        {
            // отредактировать теги перед десериализацией
            feedXML = FixFeedXMLTags(feedXML);

            Rss feed;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Rss));

            try
            {
                using (TextReader reader = new StringReader(feedXML))
                {
                    feed = (Rss)xmlSerializer.Deserialize(reader);
                }

                return feed;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Замена первых строчных букв XML-тегов на заглавные
        /// для работы парсера XmlSerializer
        /// </summary>
        /// <param name="feedXML">XML ленты в формате строки</param>
        /// <returns>XML ленты в формате строки с исправленными тегами</returns>
        static private string FixFeedXMLTags(string feedXML)
        {
            // используемые теги канала
            feedXML = ReplaceTag(feedXML, "rss");
            feedXML = ReplaceTag(feedXML, "channel");
            feedXML = ReplaceTag(feedXML, "title");
            feedXML = ReplaceTag(feedXML, "link");
            feedXML = ReplaceTag(feedXML, "description");

            // теги статей
            feedXML = ReplaceTag(feedXML, "item");
            feedXML = ReplaceTag(feedXML, "pubDate");

            return feedXML;
        }

        /// <summary>
        /// Функция, изменяющая первую букву тега на заглавную
        /// (например, <xml> -> <Xml> и </xml> -> </XML>
        /// </summary>
        /// <param name="xml">Исходный XML-текст</param>
        /// <param name="tag">Изменяемый тег</param>
        /// <returns>XML-текст с отредактированным указанным тегом</returns>
        static private string ReplaceTag(string xml, string tag)
        {
            xml = xml.Replace($"<{tag}", $"<{string.Concat(tag[0].ToString().ToUpper(), tag.AsSpan(1))}");
            xml = xml.Replace($"</{tag}", $"</{string.Concat(tag[0].ToString().ToUpper(), tag.AsSpan(1))}");

            return xml;
        }
    }
}
