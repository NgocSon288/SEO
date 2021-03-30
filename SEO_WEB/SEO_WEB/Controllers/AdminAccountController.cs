using SEO_WEB.Common;
using SEO_WEB.Models;
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

                return RedirectToAction("Index", "AdminHome");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session[Constants.SESSION] = null;

            return View("Login");
        }
    }
}