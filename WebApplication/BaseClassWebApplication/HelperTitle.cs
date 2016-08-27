using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication.BaseClassWebApplication
{
    public static class HelperTitle
    {
        public static string GetTitle(string Address)
        {
            WebClient x = new WebClient();
            string source = x.DownloadString(address: Address);
            string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;
            return title;
        }
    }
}