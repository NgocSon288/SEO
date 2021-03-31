using SEO_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminUserController : AdminBaseController
    {
        private readonly DBContext db = new DBContext();

        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public PartialViewResult UserListPartial(int page = 1, int pageSize = 5)
        {
            var data = GetUserPage(page, pageSize, out int pageMax);

            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.pageMax = pageMax;

            return PartialView(data);
        }

        [HttpGet]
        public ActionResult Update(int id, bool isMember)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.ID == id);

                user.IsMember = isMember;

                db.SaveChanges();

                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");
            }
        }


        public List<User> GetUserPage(int page, int pageSize, out int pageMax)
        {
            var users = db.Users.ToList();
            users.OrderBy(c => c.DisplayName);


            pageMax = users.Count();

            try
            {
                users = users.Skip((page - 1) * pageSize).ToList();

                return users.Take(pageSize).ToList();
            }
            catch (System.Exception)
            {
                return users;
            }
        }
    }
}