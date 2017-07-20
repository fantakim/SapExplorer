using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SapExplorer.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Code",
                "Code/",
                new { controller = "Home", action = "Code" },
                new[] { "SapExplorer.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Read",
                url: "Read/{destination}/{structure}",
                defaults: new { controller = "Home", action = "Read", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
