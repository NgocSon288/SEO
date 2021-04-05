namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Description = c.String(),
                        Alias = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Avatar = c.String(),
                        Alt = c.String(nullable: false),
                        Alias = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Metas = c.String(),
                        Title = c.String(nullable: false),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Avatar = c.String(),
                        Alt = c.String(nullable: false),
                        Metas = c.String(),
                        Content = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        TitleH1 = c.String(nullable: false),
                        Alias = c.String(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedTime = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsPriority = c.Boolean(nullable: false),
                        View = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .Index(t => t.CityID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Backlinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Link = c.String(),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Avatar = c.String(),
                        Alt = c.String(nullable: false),
                        Alias = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Reason = c.String(),
                        Description = c.String(),
                        Rating = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        PostID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Avatar = c.String(),
                        Alt = c.String(),
                        IsMember = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Homes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Keywords = c.String(),
                        Image = c.String(),
                        Metas = c.String(),
                        Title = c.String(nullable: false),
                        Alt = c.String(),
                        Footer = c.String(),
                        Logo = c.String(),
                        AltLogo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostPages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Metas = c.String(),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Backlinks", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Cities", "AreaID", "dbo.Areas");
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Backlinks", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.Posts", new[] { "CityID" });
            DropIndex("dbo.Cities", new[] { "AreaID" });
            DropTable("dbo.PostPages");
            DropTable("dbo.Homes");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Backlinks");
            DropTable("dbo.Posts");
            DropTable("dbo.Cities");
            DropTable("dbo.Areas");
        }
    }
}
