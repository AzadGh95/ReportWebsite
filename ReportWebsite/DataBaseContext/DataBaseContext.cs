using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.DataBaseContext
{
    public class DataBaseContext:System.Data.Entity.DbContext
    {
        public DataBaseContext() : base("WebContext")
        {

        }
        static DataBaseContext()
        {
            System.Data.Entity.Database.SetInitializer(
                new System.Data.Entity.DropCreateDatabaseIfModelChanges<DataBaseContext>());
        }
        public System.Data.Entity.DbSet<Entities.EN_User> Users { get; set; }
        public System.Data.Entity.DbSet<Entities.En_Element> Elements { get; set; }
        public System.Data.Entity.DbSet<Entities.EN_WebSite> WebSites { get; set; }
        public System.Data.Entity.DbSet<Entities.EN_ViewEditModel> ViewEditModels { get; set; }
        public System.Data.Entity.DbSet<Entities.EN_Item> Items { get; set; }
        public System.Data.Entity.DbSet<Entities.EN_Role> Roles { get; set; }

    }
}