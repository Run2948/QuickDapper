namespace Quick.Models.MySqlMigrations
{
    using System.Data.Entity.Migrations;

    internal sealed class MySqlConfig : DbMigrationsConfiguration<Quick.Models.MySqlDataContext>
    {
        public MySqlConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"MySqlMigrations";
            // * ±¨´í£ºNo MigrationSqlGenerator found for provider 'MySql.Data.MySqlClient'
            SetSqlGenerator("MySql.Data.MySqlClient",new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Quick.Models.MySqlDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var updater = new MySqlDbDescriptionUpdater<MySqlDataContext>(context);
            updater.UpdateDatabaseDescriptions();
        }
    }
}
