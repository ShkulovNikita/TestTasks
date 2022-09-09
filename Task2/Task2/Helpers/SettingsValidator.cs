using System;
using System.Linq;
using System.Collections.Generic;
using Task2.Models;

namespace Task2.Helpers
{
    /// <summary>
    /// Класс с методами для валидации настроек приложения 
    /// (адресов лент и частоты обновления)
    /// </summary>
    static public class SettingsValidator
    {
        /// <summary>
        /// Проверить, есть ли среди адресов пустые значения
        /// </summary>
        /// <param name="feeds">Адреса RSS-лент</param>
        /// <returns>true - валидация пройдена успешно, false - неуспешно</returns>
        static private bool CheckEmptiness(List<string> feeds)
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
        static private bool CheckUrls(List<string> feeds)
        {
            foreach (string feed in feeds)
            {
                bool result = Uri.TryCreate(feed, UriKind.Absolute, out _);
                if (!result)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Проверить, если в списке лент повторения
        /// </summary>
        /// <param name="feeds">Адреса RSS-лент</param>
        /// <returns>true - валидация пройдена успешно, false - неуспешно</returns>
        static private bool CheckRepeatings(List<string> feeds)
        {
            List<string> feedsWithoutRepeats = feeds.Distinct().ToList();
            if (feeds.Count > feedsWithoutRepeats.Count)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Выполнение валидации полей настроек
        /// </summary>
        /// <param name="settings">Объект с настройками</param>
        /// <returns>"ok", если валидация прошла успешно, либо список найденных ошибок</returns>
        static public string Validate(Settings settings)
        {
            // валидация настроек
            string validationErrors = "";

            // частота обновления должна быть положительной
            if (settings.UpdateTime <= 0)
                validationErrors += "Частота обновления должна быть положительным числом.\r\n";
            // проверка слишком большого значения
            if (settings.UpdateTime > int.MaxValue)
                validationErrors += "Задано слишком большое число.\n";
            // отсутствие списка лент
            if (settings.Feeds == null)
                validationErrors += "Не заданы RSS-ленты.\n";
            // если список лент существует - проверка его содержимого
            else
            {
                // пустота списка лент
                if (settings.Feeds.Count == 0)
                    validationErrors += "Не заданы RSS-ленты.\n";
                // присутствие пустых адресов лент
                if (!CheckEmptiness(settings.Feeds))
                    validationErrors += "Адрес ленты не может быть пустым.\n";
                // присутствие некорректных адресов лент
                if (!CheckUrls(settings.Feeds))
                    validationErrors += "Задан некорректный адрес ленты.\n";
                // повторения значений
                if (!CheckRepeatings(settings.Feeds))
                    validationErrors += "Адреса лент не должны повторяться.\n";
            }

            if (validationErrors == "")
                return "ok";
            else
                return validationErrors;
        }
    }
}
