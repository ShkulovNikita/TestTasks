using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
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
        /// Наименование файла настроек
        /// </summary>
        static private readonly string ConfigName = "config.xml";

        /// <summary>
        /// Текущие настройки
        /// </summary>
        static public Config Settings { get; set; }

        /// <summary>
        /// Обновление текущих настроек посредством чтения файла с ними
        /// </summary>
        static public void UpdateSettings()
        {
            Settings = GetSettings();
        }

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
                using (FileStream fs = new FileStream(ConfigName, FileMode.OpenOrCreate))
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
        /// Редактирование файла настроек
        /// </summary>
        /// <param name="feeds">Список выбранных лент</param>
        /// <param name="updateTime">Частота обновления</param>
        /// <returns>Результат обновления файла настроек</returns>
        static public string EditSettings(List<string> feeds, int updateTime)
        {
            // получить файл настроек как объект
            Config settings = GetSettings();
            if (settings == null)
                return "Не удалось прочитать файл настроек";

            // флаги, нужно ли обновлять список лент и частоту обновления
            bool updateFeeds = false;
            bool updateFrequency = false;

            // сравнение списков лент
            bool feedsTheSame = CompareFeeds(feeds, settings.Feeds);
            // если не совпадают - обновить список лент
            if (!feedsTheSame)
                updateFeeds = true;

            // если частота обновления не совпадает - тоже нужно обновить
            if (updateTime != settings.Update)
                updateFrequency = true;

            // обновить файл настроек, если есть какие-то изменения
            if (updateFrequency || updateFeeds)
            {
                // создание клона настроек с примененными изменениями
                Config newSettings = new Config(Settings);
                if (updateFrequency)
                    newSettings.Update = updateTime;
                if (updateFeeds)
                    newSettings.Feeds = feeds;

                // попытка изменить файл настроек
                bool updateResult = UpdateConfigFile(newSettings);

                // если успешно, то применить настройки
                if (updateResult)
                {
                    Settings = newSettings;
                    return "ok";
                }
                else
                {
                    return "Не удалось обновить файл настроек";
                }
            }

            return "ok";
        }

        /// <summary>
        /// Обновление XML-файла с настройками
        /// </summary>
        /// <param name="settings">Объект, на основе которого выполняется обновление</param>
        /// <returns>true - успешно, false - произошла ошибка</returns>
        static private bool UpdateConfigFile(Config settings)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));

            try
            {
                using (FileStream fs = new FileStream(ConfigName, FileMode.Create))
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
        /// Сравнить два списка с выбранными RSS-лентами
        /// </summary>
        /// <param name="feeds1">Один список лент для сравнения</param>
        /// <param name="feeds2">Второй список лент для сравнения</param>
        /// <returns>true - совпадают, false - не совпадают</returns>
        static private bool CompareFeeds(List<string> feeds1, List<string> feeds2)
        {
            IEnumerable<string> inFirstOnly = feeds1.Except(feeds2);
            IEnumerable<string> inSecondOnly = feeds2.Except(feeds1);
            bool allInBoth = !inFirstOnly.Any() && !inSecondOnly.Any();
            return allInBoth;
        }
    }
}
