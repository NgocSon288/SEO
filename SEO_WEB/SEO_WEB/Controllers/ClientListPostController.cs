using SEO_WEB.Common;
using SEO_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class ClientListPostController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: ClientListPost
        public ActionResult Index(string city = "", string category = "")
        {
            var data = db.Posts.Where(p => !p.IsDeleted);
            var a = data.Count();
            if (city != "" && category != "")
            {
                data = data.Where(p => p.City.Alias == city && p.Category.Alias == category);
            }
            else if (city != "")
            {
                data = data.Where(p => p.City.Alias == city);
            }
            else if (category != "")
            {
                data = data.Where(p => p.Category.Alias == category);
            }

            var b = data.Count();

            ViewBag.cities = db.Cities.ToList();
            ViewBag.categories = db.Categories.ToList();
            ViewBag.postPage = db.PostPages.FirstOrDefault();
            ViewBag.city = city;
            ViewBag.category = category;
            ViewBag.cityMetas = db.Cities.FirstOrDefault(c => c.Alias == city);

            return View(data.ToList());
        }

        public ActionResult PostsSearch(string keyword)
        {
            var data = db.Posts.Where(p => !p.IsDeleted);
            var result = new List<Post>();
            var cities = db.Cities.ToList();
            var categories = db.Categories.ToList();

            if (keyword != "")
            {
                while (keyword.IndexOf('-') >= 0)
                {
                    keyword = keyword.Replace('-', ' ');
                }

                while (keyword.IndexOf("  ") >= 0)
                {
                    keyword = keyword.Replace("  ", " ");
                }

                foreach (var item in data)
                {
                    var cityName = CompareStringHelper.Convert(cities.FirstOrDefault(c => c.ID == item.CityID).DisplayName.ToUpper());
                    var categoryName = CompareStringHelper.Convert(categories.FirstOrDefault(c => c.ID == item.CategoryID).DisplayName.ToUpper());
                    var displayName = CompareStringHelper.Convert(item.DisplayName.ToUpper());
                    var description = CompareStringHelper.Convert(item.Description.ToUpper());

                    keyword = CompareStringHelper.Convert(keyword.ToUpper());

                    if (cityName.Contains(keyword) || categoryName.Contains(keyword) || displayName.Contains(keyword) || description.Contains(keyword))
                    {
                        result.Add(item);
                    }
                }
            }


            ViewBag.cities = cities;
            ViewBag.categories = categories;
            ViewBag.postPage = db.PostPages.FirstOrDefault();
            ViewBag.city = "";
            ViewBag.category = "";
            ViewBag.keyword = keyword;

            return View(result);
        }

        // GET: ClientListPost
        public PartialViewResult GetListPostByCategoryPartial(int id)
        {
            var data = db.Posts.Where(p => !p.IsDeleted && p.Category.ID == id).ToList();


            ViewBag.cities = db.Cities.ToList();
            ViewBag.categories = db.Categories.ToList();

            return PartialView(data);
        }
    }
}