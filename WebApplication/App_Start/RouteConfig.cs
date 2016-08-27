using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.MVCRoute;

namespace WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //با این دستور فایل های دارای پسوند در سیستم مسیریابی دات نت پالایش می شوند
            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(
                "Sitemap",
                "Sitemap",
                new { controller = "Sitemap", action = "Index", name = UrlParameter.Optional, area = "" }
            );

            routes.MapRoute(
                "Rss",
                "Rss/{action}/{name}",
                new { controller = "Rss", action = "Index", name = UrlParameter.Optional, area = "" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                //constraints: new { customBrwser = new BrowserConstraint() },
                namespaces: new[] { "WebApplication.Controllers" }
            );


            routes.MapRoute(
             name: "Category",
             url: "Category/Show/{categoryId}/{categoryName}",
             defaults: new
             {
                 controller = "Category",
                 action = "Show",
                 categoryId = UrlParameter.Optional
             },
             namespaces: new[] { "WebApplication.Controllers" });



            routes.MapRoute(
                 name: "DetailsProduct",
                 url: "Product/DetailSProduct/{productId}/{productName}",
                 defaults: new
                 {
                     controller = "Product",
                     action = "DetailSProduct",
                     productId = UrlParameter.Optional
                 },
                 namespaces: new[] { "WebApplication.Controllers" });



            routes.MapRoute(
                name: "DetailsCompany",
                url: "Company/DetailsCompany/{companyId}/{companyName}",
                defaults: new
                {
                    controller = "Company",
                    action = "DetailsCompany",
                    companyId = UrlParameter.Optional
                },
                namespaces: new[] { "WebApplication.Controllers" });


            routes.MapRoute(
                name: "DetailsPackage",
                url: "Package/DetailsPackage/{packageId}/{packageName}",
                defaults: new
                {
                    controller = "Package",
                    action = "DetailsPackage",
                    packageId = UrlParameter.Optional
                },
                namespaces: new[] { "WebApplication.Controllers" });

            routes.MapRoute(
                "CatchAllRoute",
                "{*url}",
                new { controller = MVC.Search.Name, action = MVC.Search.ActionNames.Index, term = UrlParameter.Optional, area = "" }, // Parameter defaults                
                new { term = new UrlConstraint() }
            );




        }
    }
}
