/* ==============================================================================
* 命名空间：Quick.Models 
* 类 名 称：DatabaseConfig
* 创 建 者：Qing
* 创建时间：2019/06/23 15:56:08
* CLR 版本：4.0.30319.42000
* 保存的文件名：DatabaseConfig
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models
{
    public class DatabaseConfig
    {
        public static string DatabaseType = ConfigurationManager.AppSettings["Database"] ?? "MsSql";

        public static bool IsMySql => "mysql" == DatabaseType.ToLower();
        public static bool IsSqlServer => "mssql" == DatabaseType.ToLower();

        public static void Initialize()
        {
            if (IsMySql)
            {
                using (var db = new MySqlDataContext()) {}
            }

            if (IsSqlServer)
            {
                using(var db = new DataContext()){}
            }
        }
    }
}
