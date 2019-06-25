/* ==============================================================================
* 命名空间：Quick.Models 
* 类 名 称：MySqlDataContext
* 创 建 者：Qing
* 创建时间：2019/06/19 21:05:45
* CLR 版本：4.0.30319.42000
* 保存的文件名：MySqlDataContext
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

using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Quick.Models
{
    // * 报错：执行Update-Database提示Specified key was too long; max key length is 767 bytes
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public partial class MySqlDataContext : DbContext
    {
        /// <summary>
        /// 1、创建构造函数，构造函数继承DbContext类的构造函数，通过DbContext类的构造函数创建数据库连接
        /// 2、DbContext类的构造函数里面的参数是数据库连接字符串，通过该连接字符串去创建数据库
        /// </summary>
        public MySqlDataContext()
            : base("name=MySqlDataContext")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MySqlDataContext, Quick.Models.MySqlMigrations.MySqlConfig>());
        }

        public MySqlDataContext(DbConnection existConnection, bool contextOwnsConnection)
            : base(existConnection, contextOwnsConnection)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            // 这句是不要将EF生成的sql表名不要被复数 就是表名后面不要多加个S
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
