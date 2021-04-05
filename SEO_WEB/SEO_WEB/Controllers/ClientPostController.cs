using SEO_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class ClientPostController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: ClientPost
        public ActionResult Index(string city, string category, string alias, int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.ID == id);
            var relativePosts = db.Posts.Where(p => p.CityID == post.CityID).ToList();

            if (relativePosts.Count < 5)
            {
                relativePosts = db.Posts.Where(p => p.CityID == post.CityID || p.CategoryID == post.CategoryID).ToList();
            }

            post.View++;
            db.SaveChanges();

            ViewBag.users = db.Users.ToList();
            ViewBag.comments = db.Comments.Where(c => c.PostID == id).OrderByDescending(p => p.CreatedTime).ToList();
            ViewBag.relativePosts = relativePosts;
            ViewBag.cities = db.Cities.ToList();
            ViewBag.categories = db.Categories.ToList();
            ViewBag.backlinks = db.Backlinks.Where(b => b.PostID == id).ToList();

            return View(post);
        }
    }
}