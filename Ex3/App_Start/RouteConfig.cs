using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Displayer", action = "Display" }
            );

            routes.MapRoute(
                name: "Displayer",
                url: "display/{first}/{second}/{rate}",
                defaults: new { controller = "Displayer", action = "Display", rate = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Saver",
                url: "save/{ip}/{port}/{rate}/{time}/{name}",
                defaults: new { controller = "Saver", action = "Save" }
            );
        }
    }
}
