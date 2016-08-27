using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.WebPages.Html;

namespace WebApplication.BaseClassWebApplication
{
    public static class UrlExtensions
    {
        public static string ResolveTitleForUrl(this HtmlHelper htmlHelper, string title)
        {
            return string.IsNullOrEmpty(title)
                ? string.Empty
                : Regex.Replace(Regex.Replace(title, "[^\\w]", "-"), "[-]{2,}", "-");
        }

        public static string ResolveTitleForUrl(string title)
        {
            return string.IsNullOrEmpty(title)
                ? string.Empty
                : Regex.Replace(Regex.Replace(title, "[^\\w]", "-"), "[-]{2,}", "-");
        }
    }
}