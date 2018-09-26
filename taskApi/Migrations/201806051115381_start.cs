namespace taskApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationHoliday",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationType = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        AproveID = c.Int(),
                        DateCreate = c.DateTime(nullable: false),
                        DateUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AproveID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.AproveID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        Salt = c.String(),
                        RoleApp = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Calendar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        HolidayType = c.Int(nullable: false),
                        Day = c.DateTime(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Image = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationHoliday", "UserID", "dbo.User");
            DropForeignKey("dbo.ApplicationHoliday", "AproveID", "dbo.User");
            DropForeignKey("dbo.Calendar", "UserID", "dbo.User");
            DropIndex("dbo.Calendar", new[] { "UserID" });
            DropIndex("dbo.ApplicationHoliday", new[] { "AproveID" });
            DropIndex("dbo.ApplicationHoliday", new[] { "UserID" });
            DropTable("dbo.Customer");
            DropTable("dbo.Calendar");
            DropTable("dbo.User");
            DropTable("dbo.ApplicationHoliday");
        }
    }
}
