using System.Collections.Generic;

namespace Task1
{
    /// <summary>
    /// Класс с данными из файла настроек
    /// </summary>
    class Settings
    {
        public Settings()
        {
            SourceDirs = new List<string>();
        }

        public Settings(List<string> sourceDirs, string destDir)
        {
            SourceDirs = sourceDirs;
            DestinationDir = destDir;
        }

        /// <summary>
        /// Список путей до исходных папок
        /// </summary>
        public List<string> SourceDirs { get; set; }

        /// <summary>
        /// Путь до папки назначения
        /// </summary>
        public string DestinationDir { get; set; }
    }
}
