using SEO_WEB.Common;
using SEO_WEB.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult Create(City model, List<string> txtname, List<string> txtcontent, List<string> txtitemprop, List<string> txtproperty, List<string> txtequiv)
        {

            txtname.RemoveAt(0);
            txtcontent.RemoveAt(0);
            txtitemprop.RemoveAt(0);
            txtproperty.RemoveAt(0);
            txtequiv.RemoveAt(0);

            model.Metas = ConvertToMetas(txtname, txtcontent, txtitemprop, txtproperty, txtequiv);

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
        public ActionResult Update(City model, List<string> txtname, List<string> txtcontent, List<string> txtitemprop, List<string> txtproperty, List<string> txtequiv)
        {

            txtname.RemoveAt(0);
            txtcontent.RemoveAt(0);
            txtitemprop.RemoveAt(0);
            txtproperty.RemoveAt(0);
            txtequiv.RemoveAt(0);

            model.Metas = ConvertToMetas(txtname, txtcontent, txtitemprop, txtproperty, txtequiv);

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
                city.Metas = model.Metas;
                city.Title = model.Title;

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





        private string ConvertToMetas(List<string> name, List<string> content, List<string> itemprop, List<string> property, List<string> equiv)
        {
            try
            {
                var count = name.Count;
                var result = "";

                for (int i = 0; i < count; i++)
                {
                    if (name[i].Trim() == "" && content[i].Trim() == "" && itemprop[i].Trim() == "" && property[i].Trim() == "" && equiv[i].Trim() == "")
                        continue;

                    result += name[i] + Constants.BETWEEN_CHAR;
                    result += content[i] + Constants.BETWEEN_CHAR;
                    result += itemprop[i] + Constants.BETWEEN_CHAR;
                    result += property[i] + Constants.BETWEEN_CHAR;
                    result += equiv[i] + Constants.END_CHAR;

                }

                return result.Substring(0, result.Length - 1);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}