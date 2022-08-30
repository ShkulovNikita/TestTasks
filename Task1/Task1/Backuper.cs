using System;
using System.IO;
using System.Security;

namespace Task1
{
    /// <summary>
    /// Класс для выполнения резервного копирования файлов
    /// </summary>
    static public class Backuper
    {
        /// <summary>
        /// Выполнить резервное копирование
        /// </summary>
        /// <param name="settings">Параметры из настроек</param>
        /// <returns>Сообщения, которые будет необходимо 
        /// вывести в консоль после выполнения копирования</returns>
        static public string Execute(Settings settings)
        {
            // временной штамп для названия директории
            string timestamp = GetTimestamp();

            // полный путь до папки для копирования
            string path = $"{settings.DestinationDir}/{timestamp}";

            // создание конечной папки
            string createDirResult = CreateDirectory(path);
            if (createDirResult != "ok")
                return createDirResult;

            // сообщения о ходе копирования
            string results = "";

            // выполнение резервного копирования
            foreach (string sourceDir in settings.SourceDirs)
            {
                results += CopyFolder(sourceDir, path);
            }

            return results;
        }

        /// <summary>
        /// Получить временной штамп даты и времени начала
        /// выполнения копирования
        /// </summary>
        /// <returns>Временной штамп</returns>
        static private string GetTimestamp()
        {
            // получение даты и времени начала выполнения бэкапа
            DateTime backupDate = DateTime.Now;
            // преобразование во временной штамп
            string timestamp = backupDate.ToString("yyyy-MM-dd_HH-mm-ss");
            return timestamp;
        }

        /// <summary>
        /// Создание папки для копирования файлов
        /// </summary>
        /// <param name="path">Путь создаваемой папки</param>
        /// <returns>ok либо сообщение об ошибке</returns>
        static private string CreateDirectory(string path)
        {
            // создание новой директории с временным штампом
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return "ok";
            }
            catch (Exception ex)
            {
                return "Не удалось создать папку для копирования файлов: " + path;
            }
        }

        /// <summary>
        /// Копирование содержимого одной папки
        /// </summary>
        /// <param name="sourceDir">Исходная папка</param>
        /// <param name="destDir">Конечная папка</param>
        /// <returns>Результат операции</returns>
        static private string CopyFolder(string sourceDir, string destDir)
        {
            // проверить валидность имени исходного каталога
            try
            {
                FileInfo info = new FileInfo(sourceDir);
            }
            catch (ArgumentException)
            {
                return "\'" + sourceDir + "\': неправильный формат имени исходной папки.\n";
            }

            // путь для нового каталога в папке-архиве
            string path = $"{destDir}/{new DirectoryInfo(sourceDir).Name}";

            // создать в папке назначения новую папку с названием копируемой
            string mkDirResult = CreateDirectory(path);
            if (mkDirResult != "ok")
            {
                return mkDirResult;
            }

            /* ------------------------------------------*/
            /* если удалось создать папку для копирования, 
             * то обработать текущую директорию */
            /* ----------------------------------------- */

            // сообщения о выполнении копирования
            string resultMessage = "";

            // получить все файлы папки
            string[] files;
            try
            {
                files = GetFiles(sourceDir);
            }
            catch (Exception ex)
            {
                resultMessage += "Не удалось получить файлы папки " 
                    + new DirectoryInfo(sourceDir).Name + ".\n";
                files = new string[0];
            }

            // скопировать файлы
            string filesCopyResult = CopyFiles(files, path);
            resultMessage += filesCopyResult;

            // получить все подкаталоги
            string[] dirs;
            try
            {
                dirs = GetSubDirectories(sourceDir);
            }
            catch (Exception ex)
            {
                resultMessage += "Не удалось получить подкаталоги папки "
                    + new DirectoryInfo(sourceDir).Name + ".\n";
                dirs = new string[0];
            }

            // обработать все файлы в подпапках
            foreach (string dir in dirs)
            {
                string dirCopyResult = CopyFolder(dir, path);
                resultMessage += dirCopyResult;
            }

            return resultMessage;
        }

        /// <summary>
        /// Получение списка подпапок указанной папки
        /// </summary>
        /// <param name="dir">Папка, в которой выполняется поиск</param>
        /// <returns>Список подкаталогов</returns>
        static private string[] GetSubDirectories(string dir)
        {
            string[] dirs = Directory.GetDirectories(dir);
            return dirs;
        }

        /// <summary>
        /// Получение списка файлов в указанной папке
        /// </summary>
        /// <param name="dir">Папка, в которой выполняется поиск</param>
        /// <returns>Список файлов в указанном каталоге</returns>
        static private string[] GetFiles(string dir)
        {
            string[] files = Directory.GetFiles(dir);
            return files;
        }

        /// <summary>
        /// Копирование всех файлов из одной папки в другую
        /// </summary>
        /// <param name="files">Копируемые файлы</param>
        /// <param name="destDir">Папка назначения</param>
        /// <returns>Результат выполнения операции</returns>
        static private string CopyFiles(string[] files, string destDir)
        {
            // сообщения о выполнении копирования
            string resultMessage = "";
            
            // перебрать все файлы в исходной директории
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Exists)
                {
                    try
                    {
                        fileInfo.CopyTo(destDir + "/" + Path.GetFileName(file));
                    }
                    catch (SecurityException ex)
                    {
                        resultMessage += "Файл " + new DirectoryInfo(file).Name + "/" + fileInfo.Name
                            + ": " + "Отсутствует разрешение на доступ к файлу.\n";
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        resultMessage += "Файл " + new DirectoryInfo(file).Name + "/" + fileInfo.Name
                            + ": " + "Файл перемещен на другой диск.\n";
                    }
                    catch (PathTooLongException ex)
                    {
                        resultMessage += "Файл " + new DirectoryInfo(file).Name + "/" + fileInfo.Name
                            + ": " + "Указанный путь превышает разрешенную максимальную длину пути.\n";
                    }
                    catch (IOException ex)
                    {
                        resultMessage += "Файл " + new DirectoryInfo(file).Name + "/" + fileInfo.Name
                            + ": " + "Произошла ошибка.\n";
                    }
                }
            }

            return resultMessage;
        }
    }
}
