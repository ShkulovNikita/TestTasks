using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /// <param name="channelXML">XML ленты в виде строки</param>
        /// <returns>Объект, содержащий одну ленту</returns>
        static public Channel ParseChannel(string channelXML)
        {
            Channel channel;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Channel));

            try
            {
                using (TextReader reader = new StringReader(channelXML))
                {
                    channel = (Channel)xmlSerializer.Deserialize(reader);
                }

                return channel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
