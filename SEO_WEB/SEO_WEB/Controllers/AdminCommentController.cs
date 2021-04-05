using SEO_WEB.Common;
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
        private readonly DBContext   db = new DBContext();

        // GET: AdminComment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int postID, string description)
        {
            try
            {
                var comment = new Comment() {
                    Description = description,
                    PostID = postID,
                    UserID = (Session[Constants.SESSION] as User).ID,
                    Reason = "",
                    CreatedBy = (Session[Constants.SESSION] as User).ID,
                    IsDeleted = false,
                    Rating = 0,
                    UpdatedBy = (Session[Constants.SESSION] as User).ID,
                    UpdatedTime = DateTime.Now,
                    CreatedTime = DateTime.Now
                };

                db.Comments.Add(comment);
                db.SaveChanges();

                return Content("1");
            }
            catch (Exception e)
            {
                return Content("0");
            }
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

        [HttpGet]
        public PartialViewResult CommentListByPostIDPartial(int postID, int page = 1, int pageSize = 5)
        {
            var data = GetCommentPage(page, pageSize, out int pageMax, postID);

            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.pageMax = pageMax;

            ViewBag.post = db.Posts.FirstOrDefault(p => p.ID == postID);

            ViewBag.users = db.Users.ToList();
            return PartialView(data);
        }

        [HttpGet]
        public ActionResult ListCommentByPostID(int id)
        {
            ViewBag.post = db.Posts.FirstOrDefault(p=>p.ID==id);

            return View();
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


        public List<Comment> GetCommentPage(int page, int pageSize, out int pageMax, int postID = 0)
        {
            var comments = db.Comments.ToList();

            if (postID != 0)
            {
                comments = comments.Where(c => c.PostID == postID).ToList();
            }

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