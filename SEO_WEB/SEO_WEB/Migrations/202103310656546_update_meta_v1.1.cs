namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_meta_v11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Metas", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Metas", c => c.String(nullable: false));
        }
    }
}
