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
                string logLevel = Configurator.GetLogLevel();

                if (logLevel == "no file")
                {
                    Logger = LogManager.GetLogger("DebugLogger");
                    Logger.Error("Журнал: не удалось прочитать файл настроек");
                }
                else
                {
                    if (logLevel == "wrong format")
                    {
                        Logger = LogManager.GetLogger("DebugLogger");
                        Logger.Error("Журнал: не удалось получить уровень журналирования из файла настроек");
                    }
                    // проверка корректности заданного уровня журналирования
                    else if ((logLevel != "Error") && (logLevel != "Info") && (logLevel != "Debug"))
                    {
                        // если задано "неизвестное" значение - значение по умолчанию "Debug"
                        Logger = LogManager.GetLogger("DebugLogger");
                        Logger.Error("Журнал: в файле настроек задан некорректный уровень журналирования");
                    }
                    else
                    {
                        Logger = LogManager.GetLogger($"{logLevel}Logger");
                        Logger.Info($"К журналу применен уровень из файла настроек: {logLevel}");
                    }
                }
            }
            catch (Exception ex)
            {
                // если возникла ошибка, то значение по умолчанию "Debug"
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
