using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WebApplication.BaseClassWebApplication
{
    public class BrowserConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            string Name = httpContext.Request.Browser.Browser;
            double version = Convert.ToDouble(httpContext.Request.Browser.Version);
            var result = false;
            Brwser brwser = (Brwser)Enum.Parse(typeof(Brwser), Name);
            switch (brwser)
            {
                case Brwser.Safari:
                case Brwser.Firefox:
                case Brwser.Chrome:
                    result = (version >= 50) ? true : false;
                    return result;
                case Brwser.InternetExplorer:
                    result = (version >= 10) ? true : false;
                    return result;
                case Brwser.Opera:
                    result = (version >= 15) ? true : false;
                    return result;
                default:
                    return result;
            }
        }
    }

    public enum Brwser
    {
        Chrome = 1, Firefox = 2, InternetExplorer = 3, Opera = 4, Safari = 5
    }
}