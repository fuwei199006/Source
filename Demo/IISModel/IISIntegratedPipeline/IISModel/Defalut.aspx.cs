using System;

namespace IISModel
{
    public partial class Defalut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<br/>" + GetType().Assembly.Location + "</br>");
        }
    }
}