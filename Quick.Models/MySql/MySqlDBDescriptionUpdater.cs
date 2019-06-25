/* ==============================================================================
* 命名空间：Quick.Models.MySql 
* 类 名 称：MySqlDBDescriptionUpdater
* 创 建 者：Qing
* 创建时间：2019/06/25 11:49:08
* CLR 版本：4.0.30319.42000
* 保存的文件名：MySqlDBDescriptionUpdater
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Quick.Models
{
    public class MySqlDbDescriptionUpdater<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private DbTransaction _transaction;

        public MySqlDbDescriptionUpdater(TContext context)
        {
            this._context = context;
        }

        public void UpdateDatabaseDescriptions()
        {
            var contextType = typeof(TContext);
            var props = contextType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            _transaction = null;

            try
            {
                _context.Database.Connection.Open();
                _transaction = _context.Database.Connection.BeginTransaction();

                foreach (var prop in props)
                {
                    if (prop.PropertyType.InheritsOrImplements((typeof(DbSet<>))))
                    {
                        var tableType = prop.PropertyType.GetGenericArguments()[0];
                        SetTableDescriptions(tableType);
                    }
                }

                _transaction.Commit();
            }
            catch
            {
                _transaction?.Rollback();

                throw;
            }
            finally
            {
                _context.Database.Connection.Close();
            }
        }

        private void SetTableDescriptions(Type tableType)
        {
            string fullTableName = _context.GetTableName(tableType);

            Regex regex = new Regex(@"(\[\w+\]\.)?\[(?<table>.*)\]");
            Match match = regex.Match(fullTableName);

            var tableName = match.Success ? match.Groups["table"].Value : fullTableName;
            var tableAttrs = tableType.GetCustomAttributes(typeof(TableAttribute), false);

            if (tableAttrs.Length > 0)
                tableName = ((TableAttribute)tableAttrs[0]).Name;

            var dbTableDescAttr = tableType.GetCustomAttribute(typeof(DbDescriptionAttribute), false);
            string tableComment = ((DbDescriptionAttribute)dbTableDescAttr)?.Description;

            if (!string.IsNullOrEmpty(tableComment))
                SetDbDescription(tableName, null, tableComment);

            foreach (var prop in tableType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var dbDescAttr = prop.GetCustomAttribute(typeof(DbDescriptionAttribute), false);

                if (dbDescAttr == null) continue;

                var columnNameAttr = prop.GetCustomAttribute(typeof(ColumnAttribute), false);

                if (columnNameAttr != null)
                {
                    var columnName = ((ColumnAttribute)columnNameAttr).Name;
                    SetDbDescription(tableName, string.IsNullOrEmpty(columnName) ? prop.Name : columnName,
                        ((DbDescriptionAttribute)dbDescAttr)?.Description);
                }
                else
                {
                    SetDbDescription(tableName, prop.Name, ((DbDescriptionAttribute)dbDescAttr)?.Description);
                }
            }
        }

        private bool SetDbDescription(string tableName, string columnName, string comment)
        {
            tableName = (tableName ?? string.Empty).Replace("`", "");
            comment = (comment ?? string.Empty).Replace("'", "");
            if (string.IsNullOrEmpty(columnName))
            {
                ExecuteSql($"ALTER TABLE `{tableName}` COMMENT='{comment}';");
                return true;
            }
            try
            {

                var colInfo =  _context.Database.SqlQuery<MySqlColumnInfo>(@"SELECT ORDINAL_POSITION AS ColumnOrder, Column_Name AS ColumnName, data_type AS TypeName, COLUMN_COMMENT AS DescriptionText , CASE  WHEN (data_type = 'float' OR data_type = 'double' OR data_type = 'decimal') THEN NUMERIC_PRECISION ELSE CHARACTER_MAXIMUM_LENGTH END AS Length, NUMERIC_SCALE AS Scale , CASE  WHEN EXTRA = 'auto_increment' THEN 1 ELSE 0 END AS IsIdentity , CASE  WHEN COLUMN_KEY = 'PRI' THEN 1 ELSE 0 END AS IsPrimaryKey , CASE  WHEN IS_NULLABLE = 'NO' THEN 0 ELSE 1 END AS CanNull, COLUMN_DEFAULT AS DefaultValue FROM information_schema.COLUMNS WHERE table_schema = @databaseName and table_name = @tableName AND column_name = @columnName ORDER BY ORDINAL_POSITION ASC",new MySqlParameter("@databaseName", _context.GetDatabaseName()),new MySqlParameter("@tableName", tableName),new MySqlParameter("@columnName", columnName)).SingleOrDefault();

                string sql1 = string.Empty;

                if (!colInfo.CanNull)
                {
                    sql1 += " not null ";
                }

                var dict = _context.Database.SqlQuery<MySqlColumnTypeInfo>("USE INFORMATION_SCHEMA;SELECT COLUMN_TYPE AS ColumnType,EXTRA AS Extra FROM COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName;", new MySqlParameter("@tableName", tableName), new MySqlParameter("@columnName", columnName)).SingleOrDefault() ?? new MySqlColumnTypeInfo();

                if (!string.IsNullOrWhiteSpace(colInfo.DefaultValue))
                {
                    sql1 += $" default '{colInfo.DefaultValue}' ";
                }

                string sql2 = $"USE `{_context.GetDatabaseName()}`;ALTER TABLE `{tableName}` MODIFY COLUMN `{columnName}` {dict.ColumnType } {sql1} {dict.Extra} COMMENT '{comment}';"; 
                
                if (colInfo.DefaultValue != null && colInfo.DefaultValue.Equals("CURRENT_TIMESTAMP", StringComparison.OrdinalIgnoreCase))
                {
                    sql2 = $"USE `{_context.GetDatabaseName() }`;ALTER TABLE `{tableName}` CHANGE `{columnName}` `{columnName}` TIMESTAMP DEFAULT CURRENT_TIMESTAMP  {dict.Extra} COMMENT '{comment}'; ";
                }

                ExecuteSql(sql2);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        DbCommand CreateCommand(string cmdText, params SqlParameter[] parameters)
        {
            var cmd = _context.Database.Connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.Transaction = _transaction;

            foreach (var p in parameters)
                cmd.Parameters.Add(p);

            return cmd;
        }

        void ExecuteSql(string cmdText, params SqlParameter[] parameters)
        {
            var cmd = CreateCommand(cmdText, parameters);
            cmd.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Table列属性
    /// </summary>
    [Serializable]
    public class MySqlColumnInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        [DisplayName("序号")]
        public int ColumnOrder
        {
            get;
            set;
        }

        /// <summary>
        /// 列名
        /// </summary>
        [DisplayName("列名")]
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        public string TypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 列说明
        /// </summary>
        [DisplayName("列说明")]
        public string DescriptionText
        {
            get;
            set;
        }

        /// <summary>
        /// 字段长度 max 或特殊数据类型 使用 -1 表示！
        /// </summary>
        [DisplayName("长度")]
        public long? Length
        {
            get;
            set;
        }

        /// <summary>
        /// 小数点后保留位数
        /// </summary>
        [DisplayName("小数位数")]
        public int? Scale
        {
            get;
            set;
        }

        /// <summary>
        /// 是否自增列
        /// </summary>
        [DisplayName("是否为自增")]
        public bool IsIdentity
        {
            get;
            set;
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        [DisplayName("是否为主键")]
        public bool IsPrimaryKey
        {
            get;
            set;
        }

        /// <summary>
        /// 是否可为Null
        /// </summary>
        [DisplayName("是否可为空")]
        public bool CanNull
        {
            get;
            set;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        [DisplayName("默认值")]
        public string DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// 近似类型
        /// </summary>
        [DisplayName("近似类型")]
        public LikeType LikeType
        {
            get
            {
                if (this.DbType == DbType.Decimal || this.DbType == DbType.Double
                    || this.DbType == DbType.Int16
                    || this.DbType == DbType.Int32
                     || this.DbType == DbType.Int64
                    )
                {
                    return LikeType.Number;
                }
                else if (this.DbType == DbType.Date || this.DbType == DbType.DateTime
                    || this.DbType == DbType.DateTime2 || this.DbType == DbType.DateTimeOffset)
                {
                    return LikeType.DateTime;
                }
                else
                {
                    return LikeType.String;
                }
            }
        }

        /// <summary>
        /// DbType类型
        /// </summary>
        [DisplayName("DbType类型")]
        public DbType DbType
        {
            get;
            set;
        }

    }

    /// <summary>
    /// Table列数据类型
    /// </summary>
    [Serializable]
    public class MySqlColumnTypeInfo
    {
        public MySqlColumnTypeInfo()
        {
            ColumnType = string.Empty;
            Extra = string.Empty;
        }

        /// <summary>
        /// 序号
        /// </summary>
        [DisplayName("数据类型")]
        public string ColumnType
        {
            get;
            set;
        }

        /// <summary>
        /// 自增信息
        /// </summary>
        [DisplayName("自增类型")]
        public string Extra
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 相似类型
    /// </summary>
    public enum LikeType
    {
        /// <summary>
        /// 数值类型
        /// </summary>
        Number,
        /// <summary>
        /// 字符类型
        /// </summary>
        String,
        /// <summary>
        /// 日期类型
        /// </summary>
        DateTime
    }

}
