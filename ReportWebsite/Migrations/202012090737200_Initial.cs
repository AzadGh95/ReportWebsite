namespace ReportWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.En_Element",
                c => new
                    {
                        ElementId = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(nullable: false),
                        Value = c.String(maxLength: 300),
                        ItemText = c.String(maxLength: 300),
                        ItemId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ElementId)
                .ForeignKey("dbo.EN_Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.EN_WebSite", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.EN_Item",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 600),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.EN_WebSite",
                c => new
                    {
                        SiteId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 60),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Admin = c.String(maxLength: 50),
                        Type = c.Byte(nullable: false),
                        UserSite = c.String(maxLength: 50),
                        UserSuper = c.String(maxLength: 50),
                        PasswordSite = c.String(maxLength: 100),
                        PasswordSuper = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.SiteId);
            
            CreateTable(
                "dbo.EN_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        UserName = c.String(maxLength: 100),
                        Password = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 100),
                        Role = c.String(maxLength: 100),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsLock = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EN_ViewEditModel",
                c => new
                    {
                        ViewId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        ElementId = c.Int(nullable: false),
                        Value = c.String(),
                        ItemText = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ViewId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.En_Element", "SiteId", "dbo.EN_WebSite");
            DropForeignKey("dbo.En_Element", "ItemId", "dbo.EN_Item");
            DropIndex("dbo.En_Element", new[] { "SiteId" });
            DropIndex("dbo.En_Element", new[] { "ItemId" });
            DropTable("dbo.EN_ViewEditModel");
            DropTable("dbo.EN_User");
            DropTable("dbo.EN_WebSite");
            DropTable("dbo.EN_Item");
            DropTable("dbo.En_Element");
        }
    }
}
