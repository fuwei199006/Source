using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAspx
{
    public partial class Index : System.Web.UI.Page
    {
        public Index()
        {
            throw new Exception();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Response.Write("<br/>" + GetType().Assembly.Location + "</br>");
            }
        }

        
        protected void Page_PreLoad(object sender,EventArgs e)
        {
            if (IsPostBack)
            { //1
                Response.Write("<br/>" + DateTime.Now.Ticks + "</br>");
            }
            else
            {//2
                Response.Write("<br/>" + DateTime.Now.ToString("yyyy-MM-dd") + "</br>");
            }
           
        }


        protected void Page_PreRenderComplete(object sender,EventArgs e)
        {
            Response.Write("<br/>" +"12312312" + "</br>");
        }
    }
} 