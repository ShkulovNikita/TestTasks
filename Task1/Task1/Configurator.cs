using System.Text.Json;
using System.IO;
using System;

namespace Task1
{
    /// <summary>
    /// Класс для работы с файлом настроек
    /// </summary>
    static public class Configurator
    {
        /// <summary>
        /// Название файла настроек
        /// </summary>
        static private readonly string settingsName = "settings.json";

        /// <summary>
        /// Проверить, создан ли уже файл настроек
        /// </summary>
        /// <returns>false - файла нет, true - файл существует</returns>
        static public bool SettingsExist()
        {
            return (File.Exists(settingsName));
        }

        /// <summary>
        /// Получение настроек из файла
        /// </summary>
        /// <returns>Настройки в виде объекта либо null, если получить его не удалось</returns>
        static public Settings GetSettings()
        {
            // настройки в JSON-формате
            string settingsJson;

            // получение настроек из файла
            try
            {
                using (StreamReader reader = new StreamReader(settingsName))
                {
                    settingsJson = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            // парсинг настроек в объект
            try
            {
                Settings result = JsonSerializer.Deserialize<Settings>(settingsJson);
                return result;
            }
            catch (JsonException ex)
            {
                return null;
            }
        }
    }
}
