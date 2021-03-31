using SEO_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminCommentController : AdminBaseController
    {
        private readonly DBContext db = new DBContext();

        // GET: AdminComment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult CommentListPartial(int page = 1, int pageSize = 5)
        {
            var data = GetCommentPage(page, pageSize, out int pageMax);

            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.pageMax = pageMax;

            ViewBag.users = db.Users.ToList();
            return PartialView(data);
        } 

        public ActionResult Delete(int id)
        {
            try
            {
                db.Comments.Remove(db.Comments.FirstOrDefault(c => c.ID == id));

                db.SaveChanges();

                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");
            }
        }


        public List<Comment> GetCommentPage(int page, int pageSize, out int pageMax)
        {
            var comments = db.Comments.ToList();
            comments.OrderBy(c=>c.CreatedTime);


            pageMax = comments.Count();

            try
            {
                comments = comments.Skip((page - 1) * pageSize).ToList();

                return comments.Take(pageSize).ToList();
            }
            catch (System.Exception)
            {
                return comments;
            }
        }
    }
}