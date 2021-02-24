namespace ReportWebsite.Migrations
{
    using ReportWebsite.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ReportWebsite.DataBaseContext.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ReportWebsite.DataBaseContext.DataBaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            context.Roles.AddOrUpdate(r => r.RoleId,
               new EN_Role() { RoleId = 1, RoleInSystem = "SuperAdmin", RoleName = "سوپرادمین" },
               new EN_Role() { RoleId = 2, RoleInSystem = "Admin", RoleName = "ادمین" }
           );

            context.Users.AddOrUpdate(u => u.Id,
                new EN_User() { UserName = "info@dualp.ir", FirstName = "سوپر ادمین", LastName = "", RoleId = 1, Phone = "", Password = "34102030" ,CreateDate = DateTime.UtcNow , IsLock=false},
                new EN_User() { UserName = "admin", FirstName = "ادمین", LastName = "", RoleId = 2, Phone = "", Password = "34102030" , CreateDate = DateTime.UtcNow ,IsLock=false}
                );
            context.SaveChanges();
        }
    }
}
