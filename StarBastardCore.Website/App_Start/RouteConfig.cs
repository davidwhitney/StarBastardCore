using System.Web.Mvc;
using System.Web.Routing;

namespace StarBastardCore.Website.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "GameRoute_Create",
                url: "Game/Create",
                defaults: new { controller = "Game", action = "Create" }
            );

            routes.MapRoute(
                name: "GameRoute",
                url: "Game/{id}/{action}",
                defaults: new { controller = "Game", action = "View" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}