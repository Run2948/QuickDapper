using Quick.Common.Mvc.Attributes;
using System.Web.Mvc;
using QuickWeb.Filters;

namespace QuickWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JsonNetFilterAttribute());
#if !DEBUG
            filters.Add(new ViewCompressAttribute());
#endif
        }
    }
}