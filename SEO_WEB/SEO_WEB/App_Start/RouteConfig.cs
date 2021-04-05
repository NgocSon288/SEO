using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SEO_WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "dang-nhap",
                url: "dang-nhap",
                defaults: new { controller = "AdminAccount", action = "Login", keyword = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "dang-ky-tai-khoan",
                url: "dang-ky-tai-khoan",
                defaults: new { controller = "AdminAccount", action = "Signup", keyword = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "bai-viet-tim-kiem",
                url: "bai-viet/{keyword}",
                defaults: new { controller = "ClientListPost", action = "PostsSearch", keyword = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "bai-viet-tong-hop",
                url: "bai-viet-tong-hop/{city}/{category}",
                defaults: new { controller = "ClientListPost", action = "Index", city = UrlParameter.Optional , category = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "chi-tiet-bai-viet",
                url: "{city}/{category}/{alias}-{id}",
                defaults: new { controller = "ClientPost", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ClientHome", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
