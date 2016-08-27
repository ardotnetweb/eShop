using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DataLayer.Migrations;
using eShop.WebApplication.DomainClasses;
using StructureMap.Web.Pipeline;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication.BaseClassWebApplication;
using WebApplication.Controllers;
using WebApplication.SEO;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;

            //InitializationStructuremap.Init();
            setDbInitializer();
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());

            //  DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(FileSizeAttribute), typeof(ValidationAttribute));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);


            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            MvcHandler.DisableMvcResponseHeader = true;

            ScheduledTasksRegistry.Init();

        }


        protected void Application_End()
        {
            ScheduledTasksRegistry.End();
            ScheduledTasksRegistry.WakeUp(ConfigurationManager.AppSettings["SiteRootUrl"]);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }

        public class StructureMapControllerFactory : DefaultControllerFactory
        {
            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    //throw new InvalidOperationException(string.Format("Page not found: {0}",
                    //    requestContext.HttpContext.Request.RawUrl));

                    var url = requestContext.HttpContext.Request.RawUrl;
                    requestContext.RouteData.Values["controller"] = MVC.Search.Name;
                    requestContext.RouteData.Values["action"] = MVC.Search.ActionNames.Index;
                    requestContext.RouteData.Values["term"] = url.GenerateSlug().Replace("-"," ");
                    return SmObjectFactory.Container.GetInstance(typeof (SearchController)) as Controller;
                }
                return SmObjectFactory.Container.GetInstance(controllerType) as Controller;
            }
        }

        private static void setDbInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, eShop.WebApplication.DataLayer.Migrations.Configuration>());
            SmObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
        }
    }
}
