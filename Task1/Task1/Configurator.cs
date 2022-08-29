using System.Collections.Generic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;

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

        static private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };

        /// <summary>
        /// Создание файла настроек
        /// </summary>
        /// <param name="sourceDirs">Пути до исходных папок</param>
        /// <param name="destDir">Путь до папки назначения</param>
        /// <returns>Результат выполнения операции: "ok" либо выброшенное исключение</returns>
        static public string Create(List<string> sourceDirs, string destDir)
        {
            // сохранить в объект указанные пути до папок
            Settings settings = new Settings(sourceDirs, destDir);

            // преобразовать объект настроек в JSON
            string fileText = JsonSerializer.Serialize(settings, options);

            // сохранить файл настроек
            try
            {
                using (StreamWriter writer = new StreamWriter(settingsName, false))
                {
                    writer.WriteLine(fileText);
                }

                return "ok";
            }
            catch (IOException exception)
            {
                return (exception.Message);
            }
        }

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
            catch (IOException exception)
            {
                return null;
            }

            // парсинг настроек в объект
            try
            {
                Settings result = JsonSerializer.Deserialize<Settings>(settingsJson);
                return result;
            }
            catch (JsonException exception)
            {
                return null;
            }
        }
    }
}
