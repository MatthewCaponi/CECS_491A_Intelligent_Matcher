#pragma checksum "C:\Users\Matt\source\repos\CECS_491A_Intelligent_Matcher\IntelligentMatcher\WebUI\Views\UserHome\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f8e7887b0f7b88b9ad6c02f9302773b68db82a0c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserHome_Index), @"mvc.1.0.view", @"/Views/UserHome/Index.cshtml")]
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
#line 1 "C:\Users\Matt\source\repos\CECS_491A_Intelligent_Matcher\IntelligentMatcher\WebUI\Views\_ViewImports.cshtml"
using WebUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Matt\source\repos\CECS_491A_Intelligent_Matcher\IntelligentMatcher\WebUI\Views\_ViewImports.cshtml"
using WebUI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f8e7887b0f7b88b9ad6c02f9302773b68db82a0c", @"/Views/UserHome/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43d8308283e147aceb640f85e77cb8c039e61219", @"/Views/_ViewImports.cshtml")]
    public class Views_UserHome_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Matt\source\repos\CECS_491A_Intelligent_Matcher\IntelligentMatcher\WebUI\Views\UserHome\Index.cshtml"
  
    ViewData["Title"] = "User Management Home";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">User List</h1>
    <div class=""search"">
        <input type=""text"" />
        <button>Search</button>
    </div>
    <div class=""adduser""><button>Add User</button></div>
    <table class=""table table-striped"">
        <thead>
            <tr>
                <th>User ID</th>
                <th>User Name</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Account Creation Date</th>
                <th></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th scope=""row"">1</th>
                <td>Newcomer</td>
                <td>Bear</td>
                <td>Hugger</td>
                <td>December 11, 2020</td>
                <td><button>Edit</button></td>
                <td>
                    <div class=""dropdown"">
                        <button class=""userdrop"">...</button>
                        <div class=""user");
            WriteLiteral(@"actioncontent"">
                            <a href=""#"">Change Username</a>
                            <a href=""#"">Change Password</a>
                            <a href=""#"">Change E-Mail</a>
                            <a href=""#"">Suspend User</a>
                            <a href=""#"">Ban/Delete User</a>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <th scope=""row"">2</th>
                <td>Veteran</td>
                <td>Great</td>
                <td>Tiger</td>
                <td>January 3, 2009</td>
                <td><button>Edit</button></td>
                <td>
                    <div class=""dropdown"">
                        <button class=""userdrop"">...</button>
                        <div class=""useractioncontent"">
                            <a href=""#"">Change Username</a>
                            <a href=""#"">Change Password</a>
                            <a href=""#"">Change E-Ma");
            WriteLiteral(@"il</a>
                            <a href=""#"">Suspend User</a>
                            <a href=""#"">Ban/Delete User</a>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <th scope=""row"">3</th>
                <td>Laziness</td>
                <td>Robbie</td>
                <td>Rotten</td>
                <td>November 17, 2016</td>
                <td><button>Edit</button></td>
                <td>
                    <div class=""dropdown"">
                        <button class=""userdrop"">...</button>
                        <div class=""useractioncontent"">
                            <a href=""#"">Change Username</a>
                            <a href=""#"">Change Password</a>
                            <a href=""#"">Change E-Mail</a>
                            <a href=""#"">Suspend User</a>
                            <a href=""#"">Ban/Delete User</a>
                        </div>
                    </div>");
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
