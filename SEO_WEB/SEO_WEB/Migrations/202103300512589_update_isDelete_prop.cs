namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_isDelete_prop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Areas", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cities", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "IsDeleted");
            DropColumn("dbo.Categories", "IsDeleted");
            DropColumn("dbo.Cities", "IsDeleted");
            DropColumn("dbo.Areas", "IsDeleted");
        }
    }
}
