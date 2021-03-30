namespace SEO_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_foreign_key_entity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "Area_ID", "dbo.Areas");
            DropForeignKey("dbo.Posts", "City_ID", "dbo.Cities");
            DropForeignKey("dbo.Backlinks", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "User_ID", "dbo.Users");
            DropIndex("dbo.Cities", new[] { "Area_ID" });
            DropIndex("dbo.Posts", new[] { "Category_ID" });
            DropIndex("dbo.Posts", new[] { "City_ID" });
            DropIndex("dbo.Backlinks", new[] { "Post_ID" });
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            DropIndex("dbo.Comments", new[] { "User_ID" });
            RenameColumn(table: "dbo.Cities", name: "Area_ID", newName: "AreaID");
            RenameColumn(table: "dbo.Posts", name: "City_ID", newName: "CityID");
            RenameColumn(table: "dbo.Backlinks", name: "Post_ID", newName: "PostID");
            RenameColumn(table: "dbo.Posts", name: "Category_ID", newName: "CategoryID");
            RenameColumn(table: "dbo.Comments", name: "Post_ID", newName: "PostID");
            RenameColumn(table: "dbo.Comments", name: "User_ID", newName: "UserID");
            AlterColumn("dbo.Cities", "AreaID", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "CityID", c => c.Int(nullable: false));
            AlterColumn("dbo.Backlinks", "PostID", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "PostID", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Cities", "AreaID");
            CreateIndex("dbo.Posts", "CityID");
            CreateIndex("dbo.Posts", "CategoryID");
            CreateIndex("dbo.Backlinks", "PostID");
            CreateIndex("dbo.Comments", "PostID");
            CreateIndex("dbo.Comments", "UserID");
            AddForeignKey("dbo.Cities", "AreaID", "dbo.Areas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "CityID", "dbo.Cities", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Backlinks", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserID", "dbo.Users", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Backlinks", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "AreaID", "dbo.Areas");
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Backlinks", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.Posts", new[] { "CityID" });
            DropIndex("dbo.Cities", new[] { "AreaID" });
            AlterColumn("dbo.Comments", "UserID", c => c.Int());
            AlterColumn("dbo.Comments", "PostID", c => c.Int());
            AlterColumn("dbo.Backlinks", "PostID", c => c.Int());
            AlterColumn("dbo.Posts", "CityID", c => c.Int());
            AlterColumn("dbo.Posts", "CategoryID", c => c.Int());
            AlterColumn("dbo.Cities", "AreaID", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "UserID", newName: "User_ID");
            RenameColumn(table: "dbo.Comments", name: "PostID", newName: "Post_ID");
            RenameColumn(table: "dbo.Posts", name: "CategoryID", newName: "Category_ID");
            RenameColumn(table: "dbo.Backlinks", name: "PostID", newName: "Post_ID");
            RenameColumn(table: "dbo.Posts", name: "CityID", newName: "City_ID");
            RenameColumn(table: "dbo.Cities", name: "AreaID", newName: "Area_ID");
            CreateIndex("dbo.Comments", "User_ID");
            CreateIndex("dbo.Comments", "Post_ID");
            CreateIndex("dbo.Backlinks", "Post_ID");
            CreateIndex("dbo.Posts", "City_ID");
            CreateIndex("dbo.Posts", "Category_ID");
            CreateIndex("dbo.Cities", "Area_ID");
            AddForeignKey("dbo.Comments", "User_ID", "dbo.Users", "ID");
            AddForeignKey("dbo.Comments", "Post_ID", "dbo.Posts", "ID");
            AddForeignKey("dbo.Posts", "Category_ID", "dbo.Categories", "ID");
            AddForeignKey("dbo.Backlinks", "Post_ID", "dbo.Posts", "ID");
            AddForeignKey("dbo.Posts", "City_ID", "dbo.Cities", "ID");
            AddForeignKey("dbo.Cities", "Area_ID", "dbo.Areas", "ID");
        }
    }
}
