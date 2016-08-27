using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication.BaseClassWebApplication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {  //با توجه به مقدار اکشن و کنترولر عملیات انتقال صورت میگیرد
                var request = HttpContext.Current.Request.RequestContext.RouteData;
                string action = "Index";
                string controller = "Error";
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", controller }, { "action", action } });
            }
        }
    }

}



