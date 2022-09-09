using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace Task2.HtmlHelpers
{
    /// <summary>
    /// Хелпер для отображения в представлениях различных сообщений
    /// </summary>
    static public class MessageHelper
    {
        /// <summary>
        /// Вывод сообщения об ошибке
        /// </summary>
        /// <param name="error">Сообщение</param>
        /// <returns>HTML-элемент с сообщением</returns>
        static public HtmlString CheckError(this IHtmlHelper html, string error)
        {
            TagBuilder errorDiv = CreateInfoDiv("danger", error);
            if (errorDiv == null)
                return null;
            else
            {
                // запись HTML-кода
                var writer = new System.IO.StringWriter();
                errorDiv.WriteTo(writer, HtmlEncoder.Default);

                return new HtmlString(writer.ToString());
            }
        }

        /// <summary>
        /// Вывод сообщения об успешно выполненной операции
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>HTML-элемент с сообщением</returns>
        static public HtmlString CheckSuccess(this IHtmlHelper html, string message)
        {
            TagBuilder successDiv = CreateInfoDiv("success", message);
            if (successDiv == null)
                return null;
            else
            {
                // запись HTML-кода
                var writer = new System.IO.StringWriter();
                successDiv.WriteTo(writer, HtmlEncoder.Default);

                return new HtmlString(writer.ToString());
            }
        }

        /// <summary>
        /// Вывод информационного сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        static public HtmlString CheckMessage(this IHtmlHelper html, string message)
        {
            TagBuilder infoDiv = CreateInfoDiv("info", message);
            if (infoDiv == null)
                return null;
            else
            {
                // запись HTML-кода
                var writer = new System.IO.StringWriter();
                infoDiv.WriteTo(writer, HtmlEncoder.Default);

                return new HtmlString(writer.ToString());
            }
        }

        /// <summary>
        /// Создать элемент div с сообщением
        /// </summary>
        /// <param name="type">Тип сообщения: danger, success, info</param>
        /// <param name="message">Выводимое сообщение</param>
        /// <returns>div-элемент с необходимыми стилями</returns>
        static private TagBuilder CreateInfoDiv(string type, string message)
        {
            // нет сообщения для вывода
            if (message == null)
                return null;
            else
            {
                // формирование HTML-блока с сообщением
                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("alert");
                div.AddCssClass($"alert-{type}");

                // проверить, состоит ли сообщение из нескольких
                if (!message.Contains("\n"))
                    // если в сообщении одна строка, то просто вставить его в элемент
                    div.InnerHtml.Append(message);
                // если в сообщении несколько строк, то превратить их в список
                else
                {
                    string[] messages = message.Split("\n");
                    TagBuilder ul = new TagBuilder("ul");
                    foreach (string msg in messages)
                        if (msg != "")
                        {
                            TagBuilder li = new TagBuilder("li");
                            li.InnerHtml.Append(msg);
                            ul.InnerHtml.AppendHtml(li);
                        }
                    div.InnerHtml.AppendHtml(ul);
                }

                // вернуть полученный HTML-элемент
                return div;
            }
        }
    }
}