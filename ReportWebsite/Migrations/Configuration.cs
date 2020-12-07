using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using ReportWebsite.DataBaseContext;

namespace ReportWebsite.Migrations
{
    internal sealed class Configuration : 
        DbMigrationsConfiguration<DataBaseContext.DataBaseContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }


        protected override void Seed(DataBaseContext.DataBaseContext context)
        {
            base.Seed(context);
        }
    }
}