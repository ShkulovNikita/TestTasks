using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            // проверить, существует ли файл настроек
            if (!Configurator.SettingsExist())
            {
                // попытка создания конфигурационного файла, если он не найден
                bool result = UserDialogue.CreateConfigureFile();
                // если не удалось создать файл, то завершить программу
                if (!result)
                {
                    Console.WriteLine("Завершение программы: нет файла настроек.");
                    return;
                }
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

            if (backupResult != "")
                Console.WriteLine(backupResult);
            
            Console.WriteLine("Выполнение программы завершено");
            Console.ReadKey();
        }
    }
}
