using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            LogWrapper.Logger.Info("Запуск приложения");

            // проверить, существует ли файл настроек
            if (!Configurator.SettingsExist())
            {
                Console.WriteLine("Завершение программы: нет файла настроек.");
                return;
            }

            // получить настройки
            Settings settings = Configurator.GetSettings();
            if (settings == null)
            {
                Console.WriteLine("Завершение программы: не удалось получить настройки из файла.");
                return;
            }

            // выполнить копирование
            string backupResult = Backuper.Execute(settings);

            // вывести сообщения о ходе копирования
            if (backupResult != "")
                Console.WriteLine(backupResult);
            
            Console.WriteLine("Выполнение программы завершено");
            Console.ReadKey();
        }
    }
}
