namespace LinksTracker2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrationWithSeeds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Par = c.Int(nullable: false),
                        Rating = c.Decimal(precision: 18, scale: 2),
                        Slope = c.Decimal(precision: 18, scale: 2),
                        TotalHoles = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.String(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Holes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Yardage = c.Int(nullable: false),
                        Par = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Stats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIR = c.Boolean(nullable: false),
                        GIR = c.Boolean(nullable: false),
                        UpAndDown = c.Boolean(nullable: false),
                        Putts = c.Int(nullable: false),
                        Penalties = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        HoleId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Holes", t => t.HoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.HoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stats", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Stats", "HoleId", "dbo.Holes");
            DropForeignKey("dbo.Holes", "CourseId", "dbo.Courses");
            DropIndex("dbo.Stats", new[] { "UserId" });
            DropIndex("dbo.Stats", new[] { "HoleId" });
            DropIndex("dbo.Holes", new[] { "CourseId" });
            DropTable("dbo.Stats");
            DropTable("dbo.Holes");
            DropTable("dbo.Courses");
        }
    }
}
