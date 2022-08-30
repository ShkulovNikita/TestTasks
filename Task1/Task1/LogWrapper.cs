using System;
using NLog;

namespace Task1
{
    /// <summary>
    /// Класс для хранения логгера, к которому применяются
    /// настройки из файла
    /// </summary>
    static public class LogWrapper
    {
        /// <summary>
        /// Статический конструктор для задания логгера
        /// в соответствии с файлом настроек
        /// </summary>
        static LogWrapper()
        {
            // определение заданного уровня логгирования
            try
            {
                // получить уровень логгирования из файла настроек
                Settings settings = Configurator.GetSettings();
                if (settings == null)
                {
                    Logger = LogManager.GetLogger("DebugLogger");
                    Logger.Error("Не удалось получить уровень журналирования из файла настроек");
                }
                else
                {
                    string logLevel = settings.LogLevel;

                    // проверка корректности заданного уровня журналирования
                    if ((logLevel != "Error") || (logLevel != "Info") || (logLevel != "Debug"))
                    {
                        // если задано "неизвестное" значение - значение по умолчанию "Debug"
                        Logger = LogManager.GetLogger("DebugLogger");
                        Logger.Error("В файле настроек задан некорректный уровень журналирования");
                    }
                    else
                        Logger = LogManager.GetLogger($"{logLevel}Logger");
                }
            }
            catch (Exception ex)
            {
                // если возникла ошибка или не удалось открыть файл, то значение по умолчанию "Debug"
                Logger = LogManager.GetLogger("DebugLogger");
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Логгер для программы
        /// </summary>
        static public Logger Logger { get; set; }
    }
}
