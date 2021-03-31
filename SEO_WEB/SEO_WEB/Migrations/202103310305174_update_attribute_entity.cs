namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_attribute_entity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "DisplayName", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Alt", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Alias", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "DisplayName", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Alt", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Alias", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Reason", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Description", c => c.String());
            AlterColumn("dbo.Comments", "Reason", c => c.String());
            AlterColumn("dbo.Categories", "Alias", c => c.String());
            AlterColumn("dbo.Categories", "Alt", c => c.String());
            AlterColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Categories", "DisplayName", c => c.String());
            AlterColumn("dbo.Posts", "Alias", c => c.String());
            AlterColumn("dbo.Posts", "Title", c => c.String());
            AlterColumn("dbo.Posts", "Content", c => c.String());
            AlterColumn("dbo.Posts", "Alt", c => c.String());
            AlterColumn("dbo.Posts", "Description", c => c.String());
            AlterColumn("dbo.Posts", "DisplayName", c => c.String());
        }
    }
}
