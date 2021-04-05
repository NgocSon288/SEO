using SEO_WEB.Common;
using SEO_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminHomeController : AdminBaseController
    {
        private readonly DBContext db = new DBContext();

        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexMo()
        {
            return View(db.Homes.FirstOrDefault());
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult IndexMo(Home model, List<string> txtKeyword, List<string> txtname, List<string> txtcontent, List<string> txtitemprop, List<string> txtproperty, List<string> txtequiv)
        {
            var home = db.Homes.FirstOrDefault();

            model.Image = home.Image;
            txtKeyword.RemoveAt(0);
            txtname.RemoveAt(0);
            txtcontent.RemoveAt(0);
            txtitemprop.RemoveAt(0);
            txtproperty.RemoveAt(0);
            txtequiv.RemoveAt(0);

            model.Metas = ConvertToMetas(txtname, txtcontent, txtitemprop, txtproperty, txtequiv);
            model.Keywords = string.Join("|", txtKeyword);

            if (model.Keywords.Length <= 0 || model.Metas.Length <= 0 || model.Title.Length <= 0)
            {
                return View(model);
            }

            if (model.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                string extension = Path.GetExtension(model.ImageUpload.FileName);

                model.Image = fileName + new Random().Next(0, 10000).ToString() + extension;
                home.Image = model.Image;

                model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/home/"), model.Image));
            }

            if (model.ImageUploadLogo != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageUploadLogo.FileName);
                string extension = Path.GetExtension(model.ImageUploadLogo.FileName);

                model.Logo = fileName + new Random().Next(0, 10000).ToString() + extension;
                home.Logo = model.Logo;

                model.ImageUploadLogo.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/home/"), model.Logo));
            }

            home.Keywords = model.Keywords;
            home.Metas = model.Metas;
            home.Title = model.Title;
            home.Footer = model.Footer;
            home.AltLogo = model.AltLogo;

            db.SaveChanges();

            return View(model);
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