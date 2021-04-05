using SEO_WEB.Common;
using SEO_WEB.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: AdminAccount
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null)
            {
                return View(model);
            }

            if (user.Password == model.Password)
            {
                Session[Constants.SESSION] = user;

                return RedirectToAction("Index", "ClientHome");
            }
            return View(model);
        }



        public ActionResult Logout()
        {
            Session[Constants.SESSION] = null;

            return View("Login");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User model)
        {
            if (db.Users.FirstOrDefault(u => u.Username == model.Username) != null)
            {
                ModelState.AddModelError("Username", "Tài khoản đã tồn tại");

                return View(model);
            }

            if (model.ImageUpload == null)
            {
                ModelState.AddModelError("Avatar", "Hình ảnh không được bỏ trống");

                return View(model);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        model.Avatar = fileName + new Random().Next(0, 10000).ToString() + extension;

                        model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/user/"), model.Avatar));

                        model.Alt = "Hình ảnh của " + model.DisplayName;
                        model.IsMember = true;

                        // Lưu xuống DB
                        db.Users.Add(model);
                        db.SaveChanges();

                        return RedirectToAction("Login", "AdminAccount");
                    }
                }
                catch (Exception e)
                {

                }
            }

            return View(model);
        }


        public ActionResult Update()
        {
            return View(Session[Constants.SESSION] as User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(User model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userCur = Session[Constants.SESSION] as User;
                    var user = db.Users.FirstOrDefault(m => m.ID == userCur.ID);

                    if (model.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        model.Avatar = fileName + new Random().Next(0, 10000).ToString() + extension;
                        user.Avatar = model.Avatar;

                        model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/user/"), model.Avatar));

                    }

                    user.DisplayName = model.DisplayName;
                    user.Password = model.Password;
                    user.Alt = "Hình ảnh của " + model.DisplayName;

                    db.SaveChanges();

                    Session[Constants.SESSION] = user;
                    return RedirectToAction("Index", "AdminHome");
                }
                catch (Exception e)
                {

                }
            }

            return View(model);
        }

    }
}