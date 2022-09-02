#pragma checksum "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "56f5a9cfd127e1249dfa57bd3d41321ca75ce44b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
using Task2.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
using Task2.HtmlHelpers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56f5a9cfd127e1249dfa57bd3d41321ca75ce44b", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"30493c13d7ddaea2f7dcc0376a453c2288c625ce", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FeedViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "none", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 6 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "RSS-фидер";

    // сообщения со стороны сервера
    string success = null;
    if (TempData["Success"] != null)
        success = TempData["Success"].ToString();

    string error = null;
    if (TempData["Error"] != null)
        error = TempData["Error"].ToString();

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <!--Блок меню настроек-->\r\n    <div class=\"col-12\">\r\n        <!--Вывести сообщения, если есть-->\r\n        ");
#nullable restore
#line 23 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
   Write(Html.CheckError(error));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 24 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
   Write(Html.CheckSuccess(success));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        \r\n");
#nullable restore
#line 26 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
         if (Model != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <!--Форма выбора ленты-->\r\n            <div class=\"col-8\">\r\n");
#nullable restore
#line 30 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                 using (Html.BeginForm("SetFeed", "Home", FormMethod.Get))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <label for=\"feed\">Выберите ленту:</label>\r\n                    <select name=\"feed\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "56f5a9cfd127e1249dfa57bd3d41321ca75ce44b5378", async() => {
                WriteLiteral("----------");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 35 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                         foreach (string feed in Model.Feeds)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "56f5a9cfd127e1249dfa57bd3d41321ca75ce44b6826", async() => {
#nullable restore
#line 37 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                                             Write(feed);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 37 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                               WriteLiteral(feed);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 38 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </select>\r\n");
#nullable restore
#line 40 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <!--Форма настроек-->\r\n            <div class=\"col-4\">\r\n");
#nullable restore
#line 44 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                 using (Html.BeginForm("UpdateSettings", "Home", FormMethod.Post))
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                     foreach (string feed in Model.Feeds)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <input type=\"text\" name=\"feedLink\"");
            BeginWriteAttribute("value", " value=\"", 1538, "\"", 1551, 1);
#nullable restore
#line 48 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
WriteAttributeValue("", 1546, feed, 1546, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"feed-link\" />\r\n");
#nullable restore
#line 49 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <input type=\"number\" name=\"updateTime\"");
            BeginWriteAttribute("value", " value=\"", 1656, "\"", 1681, 1);
#nullable restore
#line 50 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
WriteAttributeValue("", 1664, Model.UpdateTime, 1664, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    <input type=\"submit\" class=\"btn btn-primary\" value=\"Сохранить\" />\r\n");
#nullable restore
#line 52 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n");
#nullable restore
#line 54 "D:\Tasks\TestTasks\Task2\Task2\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FeedViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
