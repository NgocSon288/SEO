using SEO_WEB.Common;
using SEO_WEB.Models;
using System.Web.Mvc;

namespace SEO_WEB.Controllers
{
    public class AdminBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = Session[Constants.SESSION] as User;

            if (user == null || !user.IsMember)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "AdminAccount",
                    action = "Login"
                }));
            }

            base.OnActionExecuting(filterContext);
        } 
    }
}