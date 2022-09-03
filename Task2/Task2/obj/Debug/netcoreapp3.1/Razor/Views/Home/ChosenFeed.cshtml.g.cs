#pragma checksum "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a7897a93831df859c0a273a790f8b1a1ed60ceb9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ChosenFeed), @"mvc.1.0.view", @"/Views/Home/ChosenFeed.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Tasks\TestTasks\Task2\Task2\Views\_ViewImports.cshtml"
using Task2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Tasks\TestTasks\Task2\Task2\Views\_ViewImports.cshtml"
using Task2.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
using Task2.HtmlHelpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
using Task2.Models.FeedModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7897a93831df859c0a273a790f8b1a1ed60ceb9", @"/Views/Home/ChosenFeed.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"30493c13d7ddaea2f7dcc0376a453c2288c625ce", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ChosenFeed : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Channel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div class=\"rss-feed\">\r\n");
#nullable restore
#line 7 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
     if (Model == null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <!--Если ничего не выбрано, то ничего не отображать-->\r\n        <div></div>\r\n");
#nullable restore
#line 11 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
    }
    else if (Model.Items.Count == 0)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
   Write(Html.CheckError("Не найдено статей в выбранной ленте"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
                                                               
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h3>RSS-канал: ");
#nullable restore
#line 18 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
                  Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n");
#nullable restore
#line 19 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
         if (Model.Description != "")
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h4>Описание: ");
#nullable restore
#line 21 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
                     Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n");
#nullable restore
#line 22 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <hr />\r\n");
#nullable restore
#line 24 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
         foreach (Item article in Model.Items)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card\">\r\n                <div class=\"card-header\">\r\n                    ");
#nullable restore
#line 28 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
               Write(article.PubDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <h5 class=\"card-title\">\r\n                        <a");
            BeginWriteAttribute("href", " href=\"", 841, "\"", 861, 1);
#nullable restore
#line 32 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
WriteAttributeValue("", 848, article.Link, 848, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" target=\"_blank\">");
#nullable restore
#line 32 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
                                                           Write(article.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                    </h5>\r\n                    <a class=\"btn btn-primary\" data-toggle=\"collapse\"");
            BeginWriteAttribute("href", " href=\"", 995, "\"", 1029, 2);
            WriteAttributeValue("", 1002, "#article_", 1002, 9, true);
#nullable restore
#line 34 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
WriteAttributeValue("", 1011, article.ArticleId, 1011, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" role=\"button\" aria-expanded=\"false\" aria-controle=\"collapseExample\">Описание</a>\r\n                    <div class=\"collapse\"");
            BeginWriteAttribute("id", " id=\"", 1154, "\"", 1185, 2);
            WriteAttributeValue("", 1159, "article_", 1159, 8, true);
#nullable restore
#line 35 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
WriteAttributeValue("", 1167, article.ArticleId, 1167, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <p class=\"article-description\">");
#nullable restore
#line 36 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
                                                  Write(article.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 40 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\ChosenFeed.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Channel> Html { get; private set; }
    }
}
#pragma warning restore 1591
