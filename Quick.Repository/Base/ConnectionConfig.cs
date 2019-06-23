/* ==============================================================================
* 命名空间：Quick.Repository.Base 
* 类 名 称：ConnectionConfig
* 创 建 者：Qing
* 创建时间：2019/06/22 12:28:52
* CLR 版本：4.0.30319.42000
* 保存的文件名：ConnectionConfig
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Repository.Base
{
    public class ConnectionConfig
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
        public static string MySqlConnectionString => ConfigurationManager.ConnectionStrings["MySqlDataContext"].ConnectionString;
    }
}
