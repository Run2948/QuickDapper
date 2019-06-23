/* ==============================================================================
* 命名空间：Quick.Repository.Base 
* 类 名 称：ConnectionFactory
* 创 建 者：Qing
* 创建时间：2019/06/22 12:29:57
* CLR 版本：4.0.30319.42000
* 保存的文件名：ConnectionFactory
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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Sql;
using MySql.Data.MySqlClient;
using Quick.Models;

namespace Quick.Repository.Base
{
    public class ConnectionFactory
    {
        public static IDbConnection CreateConnection<T>(ISqlDialect dialect, string conn) where T : IDbConnection, new()
        {
            DapperExtensions.DapperExtensions.SqlDialect = dialect;
            IDbConnection connection = new T();
            connection.ConnectionString = conn;
            connection.Open();
            return connection;
        }

        public static IDbConnection GetConnection()
        {
            if (DatabaseConfig.IsMySql) return CreateMySqlConnection();
            if (DatabaseConfig.IsSqlServer) return CreateSqlConnection();
            return CreateSqlConnection();
        }

        public static IDbConnection CreateSqlConnection()
        {
            return CreateConnection<SqlConnection>(new SqlServerDialect(), ConnectionConfig.ConnectionString);
        }

        public static IDbConnection CreateMySqlConnection()
        {
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
            return CreateConnection<MySqlConnection>(new MySqlDialect(), ConnectionConfig.MySqlConnectionString);
        }



    }
}
