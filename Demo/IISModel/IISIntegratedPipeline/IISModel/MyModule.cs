using System;
using System.Web;

namespace IISModel
{
   public class MyModule : IHttpModule
    {
        public void Dispose()
        {
           
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            application.Response.Write("<h1>This is MyModule</h1>");
        }

        
    }
}
