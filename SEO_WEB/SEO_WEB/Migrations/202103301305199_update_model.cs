namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_model : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "DisplayName", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Alt", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Alias", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "Alias", c => c.String());
            AlterColumn("dbo.Cities", "Alt", c => c.String());
            AlterColumn("dbo.Cities", "Description", c => c.String());
            AlterColumn("dbo.Cities", "DisplayName", c => c.String());
        }
    }
}
