//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:N.N.NNNNN.N
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestOutput {
using System;

public class RazorComments {
#line hidden
public RazorComments() {
}
public override void Execute() {
WriteLiteral("\r\n<p>This should ");

WriteLiteral(" be shown</p>\r\n\r\n");


#line 4 "RazorComments.cshtml"
  
    

#line default
#line hidden

#line 5 "RazorComments.cshtml"
                                       
    Exception foo = 

#line default
#line hidden

#line 6 "RazorComments.cshtml"
                                                  null;
    if(foo != null) {
        throw foo;
    }


#line default
#line hidden
WriteLiteral("\r\n\r\n");


#line 12 "RazorComments.cshtml"
   var bar = "@* bar *@"; 

#line default
#line hidden
WriteLiteral("\r\n<p>But this should show the comment syntax: ");


#line 13 "RazorComments.cshtml"
                                       Write(bar);


#line default
#line hidden
WriteLiteral("</p>\r\n\r\n");


#line 15 "RazorComments.cshtml"
Write(a

#line default
#line hidden

#line 15 "RazorComments.cshtml"
       b);


#line default
#line hidden
WriteLiteral("\r\n");

}
}
}
