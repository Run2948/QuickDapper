using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quick.Models;

namespace QuickWeb
{
    public class StartupConfig
    {
        public static void RegisterAllConfigures()
        {
            DatabaseConfig.Configure();
            AutoFacConfig.Configure();
        }
    }
}