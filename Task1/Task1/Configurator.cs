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
        /// Получение из файла настроек уровня журналирования
        /// (упрощенный метод GetSettings без журналирования, так как
        /// логгер ещё не инициализирован)
        /// </summary>
        /// <returns>Уровень журналирования</returns>
        static public string GetLogLevel()
        {
            // JSON с настройками из файла
            string settingsJson;

            // чтение из файла
            try
            {
                using (StreamReader reader = new StreamReader(settingsName))
                {
                    settingsJson = reader.ReadToEnd();
                }
            }
            catch
            {
                return "no file";
            }

            // парсинг настроек
            try
            {
                Settings settings = JsonSerializer.Deserialize<Settings>(settingsJson);
                string level = settings.LogLevel;

                return level;
            }
            catch
            {
                return "wrong format";
            }
        }

        /// <summary>
        /// Получение настроек из файла
        /// </summary>
        /// <returns>Настройки в виде объекта либо null, если получить его не удалось</returns>
        static public Settings GetSettings()
        {
            LogWrapper.Logger.Info("Получение настроек из файла");

            // настройки в JSON-формате
            string settingsJson;

            // чтение из файла
            try
            {
                LogWrapper.Logger.Debug($"Попытка чтения файла {settingsName}");
                using (StreamReader reader = new StreamReader(settingsName))
                {
                    settingsJson = reader.ReadToEnd();
                }
                LogWrapper.Logger.Debug("Настройки из файла получены");
            }
            catch (Exception ex)
            {
                LogWrapper.Logger.Error($"Не удалось прочитать файл: {ex.Message}");
                return null;
            }

            // парсинг настроек в объект
            try
            {
                LogWrapper.Logger.Debug("Выполнение десериализации настроек");
                Settings result = JsonSerializer.Deserialize<Settings>(settingsJson);

                LogWrapper.Logger.Info("Настройки из файла успешно прочтены");
                LogWrapper.Logger.Debug($"Исходные папки:");
                foreach (string dir in result.SourceDirs)
                {
                    LogWrapper.Logger.Debug(dir);
                }
                LogWrapper.Logger.Debug($"Конечная папка: {result.DestinationDir}");
                LogWrapper.Logger.Debug($"Уровень журналирования: {result.LogLevel}");

                return result;
            }
            catch (JsonException ex)
            {
                LogWrapper.Logger.Error($"Не удалось десериализовать настройки: {ex.Message}");
                return null;
            }
        }
    }
}
