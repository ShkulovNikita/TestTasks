using System.Collections.Generic;

namespace Task1
{
    /// <summary>
    /// Класс с данными из файла настроек
    /// </summary>
    public class Settings
    {
        public Settings()
        {
            SourceDirs = new List<string>();
        }

        /// <summary>
        /// Список путей до исходных папок
        /// </summary>
        public List<string> SourceDirs { get; set; }

        /// <summary>
        /// Путь до папки назначения
        /// </summary>
        public string DestinationDir { get; set; }

        /// <summary>
        /// Уровень журналирования
        /// </summary>
        public string LogLevel { get; set; }
    }
}
