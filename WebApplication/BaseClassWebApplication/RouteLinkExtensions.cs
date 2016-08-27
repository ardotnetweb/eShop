using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.BaseClassWebApplication
{
    public static class RouteLinkExtensions
    {
        public static string RouteLinkWrite(this HtmlHelper htmlHelper, string linkText, object routeValues,
            string fragment)
        {
            // There's probably better ways to do the implementation, but you get the idea
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string name = url.RouteUrl(routeValues);
            return HttpUtility.HtmlDecode(string.Format("<a href=\"{0}#{1}\">{2}</a>",
                                    url.RouteUrl(routeValues),
                                    fragment,
                                    linkText));
        }
    }
}