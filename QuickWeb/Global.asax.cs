using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuickWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            StartupConfig.RegisterAllConfigures();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception[] ex = HttpContext.Current.AllErrors;
        }
    }
}
