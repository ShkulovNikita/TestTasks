using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    /// <summary>
    /// Класс с данными из файла настроек
    /// </summary>
    class Settings
    {
        public Settings()
        {
            OriginalPaths = new List<string>();
        }

        /// <summary>
        /// Список путей до исходных папок
        /// </summary>
        public List<string> OriginalPaths { get; set; }

        /// <summary>
        /// Путь до папки назначения
        /// </summary>
        public string DestinationPath { get; set; }
    }
}
