using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            LogWrapper.Logger.Info("Запуск приложения");

            LogWrapper.Logger.Info("Попытка получения настроек программы");

            // проверить, существует ли файл настроек
            LogWrapper.Logger.Debug("Проверка существования файла настроек");
            if (!Configurator.SettingsExist())
            {
                LogWrapper.Logger.Error("Файл настроек не найден");
                Console.WriteLine("Завершение программы: нет файла настроек.");
                return;
            }
            LogWrapper.Logger.Debug("Файл настроек найден");

            // получить настройки
            LogWrapper.Logger.Debug("Попытка получения настроек из файла");
            Settings settings = Configurator.GetSettings();
            if (settings == null)
            {
                LogWrapper.Logger.Error("Не удалось получить настройки из файла");
                Console.WriteLine("Завершение программы: не удалось получить настройки из файла.");
                return;
            }
            LogWrapper.Logger.Debug("Получены настройки из файла");

            LogWrapper.Logger.Info("Успешно завершено получение настроек программы");

            // выполнить копирование
            LogWrapper.Logger.Info("Выполнение резервного копирования");
            string backupResult = Backuper.Execute(settings);

            // вывести сообщения о ходе копирования
            if (backupResult != "")
                Console.WriteLine(backupResult);

            LogWrapper.Logger.Info("Резервное копирование завершено");
            
            Console.WriteLine("Выполнение программы завершено");
            Console.ReadKey();
        }
    }
}
