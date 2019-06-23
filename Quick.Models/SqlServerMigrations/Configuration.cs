using Quick.Models;

namespace Quick.SqlServerMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class SqlServerConfig : DbMigrationsConfiguration<Quick.Models.DataContext>
    {
        public SqlServerConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"SqlServerMigrations";
        }

        protected override void Seed(Quick.Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            
            var updater = new DbDescriptionUpdater<DataContext>(context);
            updater.UpdateDatabaseDescriptions();
        }
    }
}
