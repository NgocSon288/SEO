namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize_db : DbMigration
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
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Description = c.String(),
                        Avatar = c.String(),
                        Alt = c.String(),
                        Alias = c.String(),
                        Area_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.Area_ID)
                .Index(t => t.Area_ID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Description = c.String(),
                        Avatar = c.String(),
                        Alt = c.String(),
                        Content = c.String(),
                        Meta = c.String(),
                        Title = c.String(),
                        Alias = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedTime = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Category_ID = c.Int(),
                        City_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .ForeignKey("dbo.Cities", t => t.City_ID)
                .Index(t => t.Category_ID)
                .Index(t => t.City_ID);
            
            CreateTable(
                "dbo.Backlinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Link = c.String(),
                        Post_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.Post_ID)
                .Index(t => t.Post_ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Description = c.String(),
                        Avatar = c.String(),
                        Alt = c.String(),
                        Alias = c.String(),
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
                        Post_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.Post_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Post_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        DisplayName = c.String(),
                        Email = c.String(),
                        Avatar = c.String(),
                        Alt = c.String(),
                        IsMember = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "City_ID", "dbo.Cities");
            DropForeignKey("dbo.Posts", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Backlinks", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Cities", "Area_ID", "dbo.Areas");
            DropIndex("dbo.Comments", new[] { "User_ID" });
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            DropIndex("dbo.Backlinks", new[] { "Post_ID" });
            DropIndex("dbo.Posts", new[] { "City_ID" });
            DropIndex("dbo.Posts", new[] { "Category_ID" });
            DropIndex("dbo.Cities", new[] { "Area_ID" });
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
