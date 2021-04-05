using SEO_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class ClientHomeController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: ClientHome
        public ActionResult Index()
        {
            var comments = db.Posts.OrderByDescending(p => p.View);

            ViewBag.cities = db.Cities.ToList();
            ViewBag.categories = db.Categories.ToList();
            ViewBag.areas = db.Areas.Where(a => !a.IsDeleted).ToList();
            ViewBag.postViewTop = comments.Count() > 6 ? comments.Take(6).ToList() : comments.ToList();
            ViewBag.home = db.Homes.FirstOrDefault();

            // chọn ra 4 bài viết nỗi bật nhất
            var posts = db.Posts.Where(p => !p.IsDeleted).ToList();
            if (posts.Count > 8)
            {
                posts = posts.OrderByDescending(p => p.IsPriority).ToList();

                return View(posts.Take(8).ToList());
            }

            return View(posts);
        }


        public PartialViewResult GetCitiesByAreaIDPartial(int areaID)
        {
            var cities = db.Cities.Where(c => !c.IsDeleted && c.AreaID == areaID).ToList();
            if (cities.Count > 6)
            {
                return PartialView(cities.Take(6).ToList());
            }

            return PartialView(cities);
        }

        public PartialViewResult BackgroundPartial()
        {
            ViewBag.cities = db.Cities.ToList();

            return PartialView(db.Homes.FirstOrDefault());
        }

        public PartialViewResult Footer()
        {
            return PartialView(db.Homes.FirstOrDefault());
        }

        public PartialViewResult Logo()
        {
            return PartialView(db.Homes.FirstOrDefault());
        }
    }
}