namespace ReportWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrple : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EN_Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        RoleInSystem = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            AddColumn("dbo.EN_User", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.EN_User", "RoleId");
            AddForeignKey("dbo.EN_User", "RoleId", "dbo.EN_Role", "RoleId", cascadeDelete: true);
            DropColumn("dbo.EN_User", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EN_User", "Role", c => c.String(maxLength: 100));
            DropForeignKey("dbo.EN_User", "RoleId", "dbo.EN_Role");
            DropIndex("dbo.EN_User", new[] { "RoleId" });
            DropColumn("dbo.EN_User", "RoleId");
            DropTable("dbo.EN_Role");
        }
    }
}
