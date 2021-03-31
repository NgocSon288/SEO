namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_meta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Post_ID1", "dbo.Posts");
            DropForeignKey("dbo.Metas", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            DropIndex("dbo.Comments", new[] { "Post_ID1" });
            DropIndex("dbo.Metas", new[] { "Post_ID" });
            DropColumn("dbo.Comments", "PostID");
            RenameColumn(table: "dbo.Comments", name: "Post_ID", newName: "PostID");
            AddColumn("dbo.Posts", "Metas", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "PostID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "PostID");
            AddForeignKey("dbo.Comments", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
            DropColumn("dbo.Comments", "Post_ID1");
            DropTable("dbo.Metas");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Comments", "Post_ID1", c => c.Int());
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostID" });
            AlterColumn("dbo.Comments", "PostID", c => c.Int());
            DropColumn("dbo.Posts", "Metas");
            RenameColumn(table: "dbo.Comments", name: "PostID", newName: "Post_ID");
            AddColumn("dbo.Comments", "PostID", c => c.Int(nullable: false));
            CreateIndex("dbo.Metas", "Post_ID");
            CreateIndex("dbo.Comments", "Post_ID1");
            CreateIndex("dbo.Comments", "Post_ID");
            CreateIndex("dbo.Comments", "PostID");
            AddForeignKey("dbo.Comments", "Post_ID", "dbo.Posts", "ID");
            AddForeignKey("dbo.Metas", "Post_ID", "dbo.Posts", "ID");
            AddForeignKey("dbo.Comments", "Post_ID1", "dbo.Posts", "ID");
        }
    }
}
