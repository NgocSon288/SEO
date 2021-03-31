using SEO_WEB.Common;
using SEO_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminCategoryController : AdminBaseController
    {
        private readonly DBContext db = new DBContext();
        // GET: AdminCity
        public ActionResult Index()
        {
            return View(db.Categories.Where(c => !c.IsDeleted).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Kiểm tra có chọn hình ảnh hay không
                if (model.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);

                    model.Avatar = fileName + new Random().Next(0, 10000).ToString() + extension;

                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/category/"), model.Avatar));

                    // Lưu xuống DB
                    db.Categories.Add(model);
                    db.SaveChanges();

                    return RedirectToAction("Index", "AdminCategory");
                }
            }
            catch (Exception)
            {

            }
             
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            return View(db.Categories.FirstOrDefault(c => c.ID == id));
        }

        [HttpPost]
        public ActionResult Update(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var category = db.Categories.FirstOrDefault(c => c.ID == model.ID);

                // hình ảnh được thay đổi
                if (model.ImageUpload != null)
                { 
                    string strRoot = Server.MapPath("..\\..\\Assets\\Admin\\images\\category\\") + category.Avatar;

                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);

                    model.Avatar = fileName + new Random().Next(0, 10000).ToString() + extension;
                    category.Avatar = model.Avatar;

                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/category/"), model.Avatar));


                    Constants.DeleteFile(strRoot);
                }

                category.DisplayName = model.DisplayName;
                category.Description = model.Description;
                category.Alt = model.Alt;
                category.Alias = model.Alias;

                db.SaveChanges();
                return RedirectToAction("Index", "AdminCategory");
            }
            catch (Exception e)
            {

            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var category = db.Categories.FirstOrDefault(c => c.ID == id);

                category.IsDeleted = true;

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