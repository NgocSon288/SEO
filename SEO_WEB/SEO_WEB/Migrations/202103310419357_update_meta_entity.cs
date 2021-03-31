namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_meta_entity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            CreateTable(
                "dbo.Metas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.String(),
                        Itemprop = c.String(),
                        Property = c.String(),
                        Equiv = c.String(),
                        Post_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.Post_ID)
                .Index(t => t.Post_ID);
            
            AddColumn("dbo.Comments", "Post_ID", c => c.Int());
            AddColumn("dbo.Comments", "Post_ID1", c => c.Int());
            CreateIndex("dbo.Comments", "Post_ID");
            CreateIndex("dbo.Comments", "Post_ID1");
            AddForeignKey("dbo.Comments", "Post_ID1", "dbo.Posts", "ID");
            AddForeignKey("dbo.Comments", "Post_ID", "dbo.Posts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Metas", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_ID1", "dbo.Posts");
            DropIndex("dbo.Metas", new[] { "Post_ID" });
            DropIndex("dbo.Comments", new[] { "Post_ID1" });
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            DropColumn("dbo.Comments", "Post_ID1");
            DropColumn("dbo.Comments", "Post_ID");
            DropTable("dbo.Metas");
            AddForeignKey("dbo.Comments", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
        }
    }
}
