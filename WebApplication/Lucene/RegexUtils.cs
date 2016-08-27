using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication.Lucene
{
    public static class RegexUtils
    {
        public static string RemoveHtmlTags(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
    }
}