using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Task2.Models;

namespace Task2.Helpers
{
    /// <summary>
    /// Класс для работы с файлом настроек
    /// </summary>
    static public class Configurator
    {
        /// <summary>
        /// Получить настройки из файла
        /// </summary>
        /// <returns>Настройки в виде объекта</returns>
        static public Config GetSettings()
        {
            Config settings;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
            try
            {
                using (FileStream fs = new FileStream("config.xml", FileMode.OpenOrCreate))
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
    }
}
