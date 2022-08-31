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
            string path = $"{settings.DestinationDir}\\{timestamp}";
            LogWrapper.Logger.Debug($"Конечная папка: {path}");

            // создание конечной папки
            LogWrapper.Logger.Info("Создание конечной папки для копирования");
            string createDirResult = CreateDirectory(path);
            if (createDirResult != "ok")
            {
                return createDirResult;
            }

            // сообщения о ходе копирования
            string results = "";

            LogWrapper.Logger.Debug("Начало выполнения резервного копирования");
            LogWrapper.Logger.Debug($"Количество исходных папок: {settings.SourceDirs.Count}");

            // выполнение резервного копирования
            foreach (string sourceDir in settings.SourceDirs)
            {
                results += CopyFolder(sourceDir, path);
            }

            LogWrapper.Logger.Debug("Завершение процесса резервного копирования");
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

            LogWrapper.Logger.Debug($"Временной штамп: {timestamp}");

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
                LogWrapper.Logger.Debug($"Конечная папка {path} успешно создана");
                return "ok";
            }
            catch (Exception ex)
            {
                LogWrapper.Logger.Error($"Не удалось создать конечную папку: {ex.Message}");
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
            LogWrapper.Logger.Info($"Обработка папки {sourceDir}");

            // проверить валидность имени исходного каталога
            try
            {
                FileInfo info = new FileInfo(sourceDir);
                LogWrapper.Logger.Debug($"Имя директории прошло валидацию");
            }
            catch (ArgumentException)
            {
                LogWrapper.Logger.Error($"Имя директории {sourceDir} не прошло валидацию, папка пропущена");
                return $"\'{sourceDir}\': неправильный формат имени исходной папки.\n";
            }

            // проверить существование исходного каталога
            if (!Directory.Exists(sourceDir))
            {
                LogWrapper.Logger.Error($"Исходная директория {sourceDir} не найдена");
                return $"\'{sourceDir}\': исходная папка не найдена.\n";
            }

            // путь для нового каталога в папке-архиве
            string path = $"{destDir}\\{new DirectoryInfo(sourceDir).Name}";

            // создать в папке назначения новую папку с названием копируемой
            LogWrapper.Logger.Info($"Создание конечной подпапки {path}");
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
                LogWrapper.Logger.Info("Получение списка файлов в исходной папке");
                files = GetFiles(sourceDir);
                LogWrapper.Logger.Debug($"Найдено {files.Length} файлов");
            }
            catch (Exception ex)
            {
                resultMessage += $"Не удалось получить файлы папки {new DirectoryInfo(sourceDir).Name}.\n";
                LogWrapper.Logger.Error($"Не удалось получить файлы папки {new DirectoryInfo(sourceDir).Name}: {ex.Message}");
                files = new string[0];
            }

            LogWrapper.Logger.Info("Копирование файлов");
            // скопировать файлы
            string filesCopyResult = CopyFiles(files, path);
            resultMessage += filesCopyResult;
            LogWrapper.Logger.Info($"Копирование файлов из директории {sourceDir} завершено");

            // получить все подкаталоги
            string[] dirs;
            try
            {
                LogWrapper.Logger.Info($"Получение списка подкаталогов в исходной папке");
                dirs = GetSubDirectories(sourceDir);
                LogWrapper.Logger.Debug($"Найдено {dirs.Length} подкаталогов");
            }
            catch (Exception ex)
            {
                resultMessage += $"Не удалось получить подкаталоги папки {new DirectoryInfo(sourceDir).Name}.\n";
                LogWrapper.Logger.Error($"Не удалось получить подкаталоги папки {new DirectoryInfo(sourceDir).Name}: {ex.Message}");
                dirs = new string[0];
            }

            if (dirs.Length > 0)
                LogWrapper.Logger.Info("Обработка подкаталогов");

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
                    LogWrapper.Logger.Debug($"Обработка файла {file}");
                    try
                    {
                        fileInfo.CopyTo(destDir + "/" + Path.GetFileName(file));
                        LogWrapper.Logger.Debug($"Файл {file} успешно скопирован");
                    }
                    catch (SecurityException ex)
                    {
                        resultMessage += "Файл " + new DirectoryInfo(file).Name + "/" + fileInfo.Name
                            + ": " + "Отсутствует разрешение на доступ к файлу.\n";
                        LogWrapper.Logger.Error($"Файл {file}: {ex.Message}");
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        resultMessage += "Файл " + new DirectoryInfo(file).Name + "/" + fileInfo.Name
                            + ": " + "Файл перемещен на другой диск.\n";
                        LogWrapper.Logger.Error($"Файл {file}: {ex.Message}");
                    }
                    catch (PathTooLongException ex)
                    {
                        resultMessage += "Файл " + new DirectoryInfo(file).Name + "/" + fileInfo.Name
                            + ": " + "Указанный путь превышает разрешенную максимальную длину пути.\n";
                        LogWrapper.Logger.Error($"Файл {file}: {ex.Message}");
                    }
                    catch (IOException ex)
                    {
                        resultMessage += "Файл " + new DirectoryInfo(file).Name + "/" + fileInfo.Name
                            + ": " + "Произошла ошибка.\n";
                        LogWrapper.Logger.Error($"Файл {file}: {ex.Message}");
                    }
                }
            }

            return resultMessage;
        }
    }
}
