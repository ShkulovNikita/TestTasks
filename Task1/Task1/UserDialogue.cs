using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    /// <summary>
    /// Класс с методами для общения с пользователем
    /// </summary>
    static class UserDialogue
    {
        /// <summary>
        /// Создание файла настроек (если он уже не создан)
        /// </summary>
        /// <returns>true - файл был успешно создан, false - не удалось создать файл</returns>
        static public bool CreateConfigureFile()
        {
            Console.WriteLine("Не найден файл настроек.");
            Console.WriteLine("Введите исходные папки для резервного копирования (для окончания ввода введите 0).");

            // ввод исходных папок

            List<string> sourceDirs = new List<string>();
            string newDir = Console.ReadLine();

            // должен быть введен как минимум один путь
            while (newDir == "0")
            {
                Console.WriteLine("Введите как минимум один путь.");
                newDir = Console.ReadLine();
            }

            // добавление первого исходного пути
            sourceDirs.Add(newDir);
            Console.WriteLine("Путь сохранен.");

            Console.WriteLine("Введите другие пути либо 0.");
            // добавление большего числа путей
            newDir = Console.ReadLine();
            while (newDir != "0")
            {
                sourceDirs.Add(newDir);
                Console.WriteLine("Путь сохранен");
                newDir = Console.ReadLine();
            }

            Console.WriteLine("Введите папку назначения.");
            string destDir = Console.ReadLine();

            // создание файла настроек
            string creationResult = Configurator.Create(sourceDirs, destDir);

            if (creationResult == "ok")
            {
                Console.WriteLine("Файл настроек успешно создан.");
                return true;
            }
            else
            {
                Console.WriteLine("Не удалось создать файл настроек.");
                return false;
            }
        }

    }
}
