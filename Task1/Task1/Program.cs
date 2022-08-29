using System;
using System.Collections.Generic;

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
            
            
            
            Console.WriteLine("Выполнение программы завершено");
        }
    }
}
