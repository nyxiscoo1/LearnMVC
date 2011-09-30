using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;

namespace LearnMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Images", // Route name
                "images/{imageName}", // URL with parameters
                new { controller = MVC.Image.Name, action = MVC.Image.ActionNames.Index, imageName = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "RssSyndication", // Route name
                "feeds/rss.xml", // URL with parameters
                new { controller = MVC.Syndication.Name, action = MVC.Syndication.ActionNames.Rss } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = MVC.Files.Name, action = MVC.Files.ActionNames.Index, id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            var container = AutofacBootstrapper.Configure();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}