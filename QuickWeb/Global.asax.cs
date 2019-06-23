using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Quick.Models;

namespace QuickWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DatabaseConfig.Initialize();
            AutoFacConfig.RegisterAllApps();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception[] ex = HttpContext.Current.AllErrors;
        }
    }
}
