﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IISIntegratedPipeline
{
    public partial class Defalut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<br/>" + GetType().Assembly.Location + "</br>");
        }
    }
}