using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
        public DbSet<Post> Posts { set; get; }
        public DbSet<User> Users { set; get; }
         
    }
}