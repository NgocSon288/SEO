namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletemetaproperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "Meta");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Meta", c => c.String());
        }
    }
}
