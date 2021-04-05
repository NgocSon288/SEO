using System.Data.Entity;

namespace SEO_WEB.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("SeoConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Area> Areas { set; get; }
        public DbSet<Backlink> Backlinks { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<City> Cities { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Home> Homes { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<PostPage> PostPages { set; get; }
    }
}