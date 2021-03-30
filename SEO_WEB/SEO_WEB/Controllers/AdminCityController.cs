using SEO_WEB.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminCityController : AdminBaseController
    {
        private readonly DBContext db = new DBContext();
        // GET: AdminCity
        public ActionResult Index()
        {
            ViewBag.Area = db.Areas.ToList();

            return View(db.Cities.Where(c=>!c.IsDeleted).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Area = new SelectList(db.Areas.Where(a => !a.IsDeleted).ToList(), "ID", "DisplayName");


            return View();
        }

        [HttpPost]
        public ActionResult Create(City model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Area = new SelectList(db.Areas.Where(a=>!a.IsDeleted).ToList(), "ID", "DisplayName");

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
                     
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/city/"), model.Avatar));

                    // Lưu xuống DB
                    db.Cities.Add(model);
                    db.SaveChanges();

                    return RedirectToAction("Index", "AdminCity");
                }
            }
            catch (Exception)
            {

            }

            ViewBag.Area = new SelectList(db.Areas.Where(a => !a.IsDeleted).ToList(), "ID", "DisplayName");

            return View(model);
        }
         
        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.Area = new SelectList(db.Areas.Where(a => !a.IsDeleted).ToList(), "ID", "DisplayName");

            return View(db.Cities.FirstOrDefault(c=>c.ID == id));
        }

        [HttpPost]
        public ActionResult Update(City model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Area = new SelectList(db.Areas.Where(a => !a.IsDeleted).ToList(), "ID", "DisplayName");

                return View(model);
            }

            try
            {
                var city = db.Cities.FirstOrDefault(c => c.ID == model.ID);

                // hình ảnh được thay đổi
                if (model.ImageUpload != null)
                {
                    var deletePath = city.Avatar; 
                    string strRoot = Server.MapPath("..\\..\\Assets\\Admin\\images\\city\\") + deletePath;

                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    
                    model.Avatar = fileName + new Random().Next(0, 10000).ToString() + extension;
                    city.Avatar = model.Avatar;

                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/city/"), model.Avatar));


                    FileInfo myfileinf = new FileInfo(strRoot);
                    myfileinf.Delete();
                }

                city.DisplayName = model.DisplayName;
                city.Description = model.Description;
                city.Alt = model.Alt;
                city.Alias = model.Alias;
                city.AreaID = model.AreaID;

                db.SaveChanges();
                return RedirectToAction("Index", "AdminCity");
            }
            catch (Exception e)
            {

            }

            ViewBag.Area = new SelectList(db.Areas.Where(a => !a.IsDeleted).ToList(), "ID", "DisplayName");

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var city = db.Cities.FirstOrDefault(c => c.ID == id);

                city.IsDeleted = true;

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