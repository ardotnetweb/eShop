using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StructureMap;
using Microsoft.Owin.Security;
using System.Security.Policy;

namespace WebApplication.BaseClassWebApplication
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var description = filterContext.ActionDescriptor;
            string actionName = description.ActionName;
            string controllerName = description.ControllerDescriptor.ControllerName;
            var parameter = description.GetParameters();
            var contextN = filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri;

            //UrlHelper url = new UrlHelper();
            //UrlHelper url = new UrlHelper(filterContext.RequestContext);
            //url.Action(MVC.Error.ActionNames.Index, MVC.Error.Name);

            //filterContext.RequestContext.HttpContext.Request.IsAjaxRequest

        }
    }
}



//filterContext.RouteData.Values["area"] = "admin";
//filterContext.RouteData.Values["controller"] = "Home";
//filterContext.RouteData.Values["action"] = "index";
//filterContext.Result = new ViewResult { ViewName = "index" };