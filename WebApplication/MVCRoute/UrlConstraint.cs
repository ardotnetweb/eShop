using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using WebApplication.SEO;

namespace WebApplication.MVCRoute
{
    public class UrlConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext,
                          Route route, 
                          string parameterName,
                          RouteValueDictionary values,
                          RouteDirection routeDirection)
        {
            var url = httpContext.Request.RawUrl;
            values["term"] = url.GenerateSlug().Replace("-", " ");
            return true;
        }
    }
}