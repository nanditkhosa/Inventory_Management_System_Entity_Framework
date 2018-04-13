namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class March30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facility",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Landmark = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        InitialCount = c.Int(nullable: false),
                        CurrentCount = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedUser = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facility", t => t.FacilityId)
                .Index(t => t.FacilityId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErrorLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppName = c.String(nullable: false),
                        Thread = c.String(nullable: false),
                        Level = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        Exception = c.String(nullable: false),
                        LogDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedTimeStamp = c.DateTime(nullable: false),
                        LastModifiedTimeStamp = c.DateTime(nullable: false),
                        LastModifiedUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FacilityUser",
                c => new
                    {
                        FacilityFKId = c.Int(nullable: false),
                        UserFKId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacilityFKId, t.UserFKId })
                .ForeignKey("dbo.Facility", t => t.FacilityFKId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserFKId, cascadeDelete: true)
                .Index(t => t.FacilityFKId)
                .Index(t => t.UserFKId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FacilityUser", "UserFKId", "dbo.User");
            DropForeignKey("dbo.FacilityUser", "FacilityFKId", "dbo.Facility");
            DropForeignKey("dbo.Resource", "FacilityId", "dbo.Facility");
            DropIndex("dbo.FacilityUser", new[] { "UserFKId" });
            DropIndex("dbo.FacilityUser", new[] { "FacilityFKId" });
            DropIndex("dbo.Resource", new[] { "FacilityId" });
            DropTable("dbo.FacilityUser");
            DropTable("dbo.ErrorLog");
            DropTable("dbo.User");
            DropTable("dbo.Resource");
            DropTable("dbo.Facility");
        }
    }
}
