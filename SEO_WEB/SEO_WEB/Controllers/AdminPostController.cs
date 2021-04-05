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
    public class AdminPostController : AdminBaseController
    {
        private readonly DBContext db = new DBContext();

        // GET: AdminPost
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult PostListPartial(string key = "", int page = 1, int pageSize = 5, int cityID = 0, int categoryID = 0)
        {
            var data = GetPostPage(key, page, pageSize, out int pageMax, cityID, categoryID);

            ViewBag.key = key;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.pageMax = pageMax;
            ViewBag.cityID = cityID;
            ViewBag.categoryID = categoryID;

            ViewBag.cities = db.Cities.Where(c => !c.IsDeleted).ToList();
            ViewBag.categories = db.Categories.Where(c => !c.IsDeleted).ToList();

            ViewBag.comments = db.Comments.ToList();

            return PartialView(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Setup();

            return View();
        }

        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Post model, List<string> txtname, List<string> txtcontent, List<string> txtitemprop, List<string> txtproperty, List<string> txtequiv, List<string> txtLink, List<string> txtText)
        {
            try
            {
                txtname.RemoveAt(0);
                txtcontent.RemoveAt(0);
                txtitemprop.RemoveAt(0);
                txtproperty.RemoveAt(0);
                txtequiv.RemoveAt(0);
                txtLink.RemoveAt(0);
                txtText.RemoveAt(0);

                model.Metas = ConvertToMetas(txtname, txtcontent, txtitemprop, txtproperty, txtequiv);
                model.Backlinks = ConvertToBacklink(txtText, txtLink);


                model.Avatar = "";

                if (db.Posts.FirstOrDefault(p => p.DisplayName.ToUpper() == model.DisplayName.ToUpper()) != null)
                {
                    ModelState.AddModelError("DisplayName", "Tên bài viết đã tồn tại");
                }

                if (db.Posts.FirstOrDefault(p => p.Title.ToUpper() == model.Title.ToUpper()) != null)
                {
                    ModelState.AddModelError("Title", "Title bài viết đã tồn tại");
                }

                if (db.Posts.FirstOrDefault(p => p.Alias.ToUpper() == model.Alias.ToUpper()) != null)
                {
                    ModelState.AddModelError("Alias", "Alias bài viết đã tồn tại");
                }

                if (db.Posts.FirstOrDefault(p => p.Alias.ToUpper() == model.Alias.ToUpper()) != null)
                {
                    ModelState.AddModelError("TitleH1", "Thẻ h1 bài viết đã tồn tại");
                }

                if (model.ImageUpload == null)
                {
                    ModelState.AddModelError("Avatar", "Hình ảnh không bỏ trống");
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

                            model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/post/"), model.Avatar));
                        }
                    }
                    catch (Exception)
                    {

                    }

                    model.CreatedBy = (Session[Constants.SESSION] as User).DisplayName;
                    model.CreatedTime = DateTime.Now;
                    model.IsDeleted = false;
                    model.UpdatedBy = "...";
                    model.UpdatedTime = DateTime.Now;

                    db.Posts.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index", "AdminPost");
                }
            }
            catch (Exception e)
            {
                Setup();
                ViewBag.texts = txtText;
                ViewBag.links = txtLink;

                return View(model);
            }

            Setup();
            ViewBag.texts = txtText;
            ViewBag.links = txtLink;

            return View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Setup();
            var model = db.Posts.FirstOrDefault(p => p.ID == id);

            model.Backlinks = db.Backlinks.Where(b => b.PostID == id).ToList();
             
            return View(model);
        }

        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(Post model, List<string> txtname, List<string> txtcontent, List<string> txtitemprop, List<string> txtproperty, List<string> txtequiv, List<string> txtLink, List<string> txtText)
        {
            Post post = db.Posts.FirstOrDefault(p => p.ID == model.ID);
            try
            {
                txtname.RemoveAt(0);
                txtcontent.RemoveAt(0);
                txtitemprop.RemoveAt(0);
                txtproperty.RemoveAt(0);
                txtequiv.RemoveAt(0);
                txtLink.RemoveAt(0);
                txtText.RemoveAt(0);

                model.Metas = ConvertToMetas(txtname, txtcontent, txtitemprop, txtproperty, txtequiv);

                db.Backlinks.RemoveRange(db.Backlinks.Where(b => b.PostID == post.ID));
                post.Backlinks = ConvertToBacklink(txtText, txtLink);

                model.Avatar = "";

                if (db.Posts.FirstOrDefault(p => p.ID != model.ID && p.DisplayName.ToUpper() == model.DisplayName.ToUpper()) != null)
                {
                    ModelState.AddModelError("DisplayName", "Tên bài viết đã tồn tại");
                }

                if (db.Posts.FirstOrDefault(p => p.ID != model.ID && p.Title.ToUpper() == model.Title.ToUpper()) != null)
                {
                    ModelState.AddModelError("Title", "Title bài viết đã tồn tại");
                }

                if (db.Posts.FirstOrDefault(p => p.ID != model.ID && p.Alias.ToUpper() == model.Alias.ToUpper()) != null)
                {
                    ModelState.AddModelError("Alias", "Alias bài viết đã tồn tại");
                }

                if (db.Posts.FirstOrDefault(p => p.ID != model.ID && p.Alias.ToUpper() == model.Alias.ToUpper()) != null)
                {
                    ModelState.AddModelError("TitleH1", "Thẻ h1 bài viết đã tồn tại");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (model.ImageUpload != null)
                        {
                            string strRoot = Server.MapPath("..\\..\\Assets\\Admin\\images\\post\\") + post.Avatar;

                            string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                            string extension = Path.GetExtension(model.ImageUpload.FileName);

                            model.Avatar = fileName + new Random().Next(0, 10000).ToString() + extension;
                            post.Avatar = model.Avatar;

                            model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Assets/Admin/images/post/"), model.Avatar));

                            Constants.DeleteFile(strRoot);
                        }
                    }
                    catch (Exception)
                    {

                    }

                    post.DisplayName = model.DisplayName;
                    post.Description = model.Description;
                    post.Alt = model.Alt;
                    post.Metas = model.Metas;
                    post.Content = model.Content;
                    post.Title = model.Title;
                    post.TitleH1 = model.TitleH1;
                    post.Alias = model.Alias;
                    post.CityID = model.CityID;
                    post.CategoryID = model.CategoryID;
                    post.IsPriority = model.IsPriority;
                    post.UpdatedBy = (Session[Constants.SESSION] as User).DisplayName;
                    post.UpdatedTime = DateTime.Now;

                    db.SaveChanges();
                    return RedirectToAction("Index", "AdminPost");
                }
            }
            catch (Exception e)
            {
                Setup();
                return View(post);
            }

            Setup();
            return View(post);
        }

        [HttpPost]
        public ActionResult Delete(int id, bool isDelete)
        {
            try
            {
                var post = db.Posts.FirstOrDefault(p => p.ID == id);
                post.IsDeleted = isDelete;

                db.SaveChanges();
                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");
            }
        }



        #region Method

        public List<Post> GetPostPage(string key, int page, int pageSize, out int pageMax, int cityID, int categoryID)
        {
            var posts = db.Posts.ToList();

            if (key != null && key != "")
            {
                posts = posts.Where(p => p.DisplayName.ToLower().Contains(key.ToLower()) || p.Alias.ToLower().Contains(key.ToLower())).ToList();
            }


            if (cityID != 0)
            {
                posts = posts.Where(p => p.CityID == cityID).ToList();
            }

            if (categoryID != 0)
            {
                posts = posts.Where(p => p.CategoryID == categoryID).ToList();
            }

            pageMax = posts.Count();

            try
            {
                posts = posts.Skip((page - 1) * pageSize).ToList();

                return posts.Take(pageSize).ToList();
            }
            catch (System.Exception)
            {
                return posts;
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

        private List<Backlink> ConvertToBacklink(List<string> texts, List<string> links)
        {
            var result = new List<Backlink>();
            for (int i = 0; i < texts.Count; i++)
            {
                if (texts[i].Trim() == "" || links[i].Trim() == "")
                    continue;

                result.Add(new Backlink() { DisplayName = texts[i], Link = links[i] });
            }

            return result;
        }

        private void Setup()
        {
            var cities = db.Cities.Where(c=>!c.IsDeleted);
            var categories = db.Categories.Where(c => !c.IsDeleted);

            SelectList citySL = new SelectList(cities, "ID", "DisplayName");
            SelectList categorySL = new SelectList(categories, "ID", "DisplayName");

            ViewBag.citySL = citySL;
            ViewBag.categorySL = categorySL;
        }

        #endregion

    }
}