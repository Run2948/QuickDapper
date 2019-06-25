using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Quick.Models
{
    public class DbDescriptionUpdater<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private DbTransaction _transaction;

        public DbDescriptionUpdater(TContext context)
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

        private void SetDbDescription(string tableName, string columnName, string description)
        {
            string desc;

            if (string.IsNullOrEmpty(columnName))
                desc = "select [value] from fn_listextendedproperty('MS_Description','schema','dbo','table',N'" + tableName + "',null,null);";
            else
                desc = "select [value] from fn_listextendedproperty('MS_Description','schema','dbo','table',N'" + tableName + "','column',null) where objname = N'" + columnName + "';";

            var prevDesc = (string)ExecuteSqlScalar(desc);

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@table", tableName),
                new SqlParameter("@desc", description)
            };

            string funcName = "sp_addextendedproperty";

            if (!string.IsNullOrEmpty(prevDesc))
                funcName = "sp_updateextendedproperty";

            string query = @"EXEC " + funcName + @" N'MS_Description', @desc, N'Schema', 'dbo', N'Table', @table";
         
            if (!string.IsNullOrEmpty(columnName))
            {
                query += ", N'Column', @column";
                parameters.Add(new SqlParameter("@column", columnName));
            }

            ExecuteSql(query, parameters.ToArray());
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
        object ExecuteSqlScalar(string cmdText, params SqlParameter[] parameters)
        {
            var cmd = CreateCommand(cmdText, parameters);
            return cmd.ExecuteScalar();
        }
    }
}
