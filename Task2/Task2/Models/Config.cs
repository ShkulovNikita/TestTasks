using System.Collections.Generic;

namespace Task2.Models
{
    /// <summary>
    /// Класс для использования данных, получаемых из XML-файла настроек, в виде объекта
    /// </summary>
    public class Config
    {
        public Config() { }

        /// <summary>
        /// Клонирование объекта с настройками
        /// </summary>
        /// <param name="original"></param>
        public Config (Config original)
        {
            Feeds = original.Feeds;
            Update = original.Update;
            Proxy = original.Proxy;
            Login = original.Login;
            Password = original.Password;
        }

        /// <summary>
        /// Добавленные RSS-ленты
        /// </summary>
        public List<string> Feeds { get; set; } = new List<string>();

        /// <summary>
        /// Частота обновления в миллисекундах
        /// </summary>
        public int Update { get; set; }

        /// <summary>
        /// Адрес прокси-сервера
        /// </summary>
        public string Proxy { get; set; }

        /// <summary>
        /// Логин для авторизации в прокси-сервере
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль для авторизации в прокси-сервере
        /// </summary>
        public string Password { get; set; }
    }
}
