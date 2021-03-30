using SEO_WEB.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminAreaController : AdminBaseController
    {
        private readonly DBContext db = new DBContext();

        // GET: Area
        public ActionResult Index()
        {
            return View(db.Areas.Where(a => !a.IsDeleted).ToList());
        }

        [HttpPost]
        public ActionResult Create(string displayName, string description, string alias)
        {
            if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(alias))
            {
                return Content("0");
            }

            try
            {
                var area = new Area() { DisplayName = displayName, Description = description, Alias = alias };

                db.Areas.Add(area);
                db.SaveChanges();

                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");
            }
        }
         
        [HttpPost]
        public ActionResult Update(int id, string displayName, string description, string alias)
        {
            if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(alias))
            {
                return Content("0");
            }

            try
            {
                var area = db.Areas.FirstOrDefault(a => a.ID == id);

                if (area == null)
                    return Content("0");


                area.DisplayName = displayName;
                area.Description = description;
                area.Alias = alias;
                 
                db.SaveChanges();

                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var area = db.Areas.FirstOrDefault(a => a.ID == id);
                area.IsDeleted = true;

                db.SaveChanges();

                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");
            }
        }
    }
}