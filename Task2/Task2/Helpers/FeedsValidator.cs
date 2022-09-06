using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.Helpers
{
    /// <summary>
    /// Класс с методами для валидации адресов RSS-лент
    /// </summary>
    static public class FeedsValidator
    {
        /// <summary>
        /// Проверить, есть ли среди адресов пустые значения
        /// </summary>
        /// <param name="feeds">Адреса RSS-лент</param>
        /// <returns>true - валидация пройдена успешно, false - неуспешно</returns>
        static public bool CheckEmptiness(List<string> feeds)
        {
            List<string> empties = feeds.Where(x => x == "").ToList();
            if (empties.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Проверить, есть ли среди адресов некорректные
        /// </summary>
        /// <param name="feeds">Адреса RSS-лент</param>
        /// <returns>true - валидация пройдена успешно, false - неуспешно</returns>
        static public bool CheckUrls(List<string> feeds)
        {
            foreach (string feed in feeds)
            {
                bool result = Uri.TryCreate(feed, UriKind.Absolute, out _);
                if (!result)
                    return false;
            }

            return true;
        }
    }
}
