#pragma checksum "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\Shared\_TransactionList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7d9c261a62fda978499be25e640b0280fadbc0c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__TransactionList), @"mvc.1.0.view", @"/Views/Shared/_TransactionList.cshtml")]
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
#line 1 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\_ViewImports.cshtml"
using BankingApp.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\_ViewImports.cshtml"
using BankingApp.UI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\_ViewImports.cshtml"
using BankingApp.UI.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\_ViewImports.cshtml"
using BankingApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\_ViewImports.cshtml"
using System.Web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d9c261a62fda978499be25e640b0280fadbc0c7", @"/Views/Shared/_TransactionList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cb75096832e2bc70101c0c36d75a14322ed36af", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__TransactionList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BankingApp.Models.Transaction>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<table class=""table"">
    <thead>
        <tr>
            <th>
                Amount
            </th>
            <th>
                Type
            </th>
            <th>
                Date Stamp
            </th>

            <th></th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 20 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\Shared\_TransactionList.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    $");
#nullable restore
#line 24 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\Shared\_TransactionList.cshtml"
                Write(Html.DisplayFor(modelItem => item.Amount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 27 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\Shared\_TransactionList.cshtml"
               Write(Html.DisplayFor(modelItem => item.TransactionType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 30 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\Shared\_TransactionList.cshtml"
               Write(Html.DisplayFor(modelItem => item.DateStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n            </tr>\r\n");
#nullable restore
#line 34 "C:\Users\Henri\Revature\Workarea\Project1\BankingApp.UI\Views\Shared\_TransactionList.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BankingApp.Models.Transaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591