using SEO_WEB.Common;
using SEO_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminPostPageController : AdminBaseController
    {
        private readonly DBContext db = new DBContext();

        // GET: AdminPostPage
        public ActionResult Index()
        {
            return View(db.PostPages.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Index(PostPage model, List<string> txtname, List<string> txtcontent, List<string> txtitemprop, List<string> txtproperty, List<string> txtequiv)
        {
            var postPage = db.PostPages.FirstOrDefault();

            txtname.RemoveAt(0);
            txtcontent.RemoveAt(0);
            txtitemprop.RemoveAt(0);
            txtproperty.RemoveAt(0);
            txtequiv.RemoveAt(0);

            model.Metas = ConvertToMetas(txtname, txtcontent, txtitemprop, txtproperty, txtequiv);

            postPage.Metas = model.Metas;
            postPage.Title = model.Title;

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