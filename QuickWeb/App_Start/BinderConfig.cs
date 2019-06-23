using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common.Mvc.Attributes;

namespace QuickWeb
{
    public class BinderConfig
    {
        public static void Configure()
        {
            // 自动截取空格和进行全角转换
            ModelBinders.Binders.Add(typeof(string), new TrimToDbcModelBinder());
        }
    }
}