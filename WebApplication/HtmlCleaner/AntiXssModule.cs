using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace HtmlCleaner
{
    public class AntiXssModule : IHttpModule
    {
        #region Fields (3)

        private static readonly Regex CleanAllTags = new Regex("<[^>]+>", RegexOptions.Compiled);
        private static readonly IList<string> IgnoreList = new List<string>
		{
			"__EVENTVALIDATION",
			"__LASTFOCUS",
			"__EVENTTARGET",
			"__EVENTARGUMENT",
			"__VIEWSTATE",
			"__SCROLLPOSITIONX",
			"__SCROLLPOSITIONY",
			"__VIEWSTATEENCRYPTED",
			"__ASYNCPOST",
			"pagedata"
		};
        private static readonly PropertyInfo ReadonlyProperty = typeof(NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

        #endregion Fields

        #region Methods (6)

        // Public Methods (2) 

        public void Dispose()
        { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += cleanUpInput;
        }
        // Private Methods (4) 

        private static void cleanUpAndEncodeCookies(HttpCookieCollection cookiesCollection)
        {
            foreach (var key in cookiesCollection.AllKeys)
            {
                var cookie = cookiesCollection[key];
                if (cookie == null) continue;

                foreach (string cookieKey in cookie.Values.AllKeys)
                {
                    var origData = cookie.Values[cookieKey];
                    if (string.IsNullOrEmpty(origData)) continue;
                    origData = origData.Trim();
                    var modifiedData = HttpUtility.HtmlEncode(CleanAllTags.Replace(origData, string.Empty));
                    if (origData != modifiedData)
                    {
                        cookie.Values[cookieKey] = modifiedData;
                    }
                }
            }
        }

        private static void cleanUpAndEncodeFormFields(NameValueCollection formFieldsCollection)
        {
            ReadonlyProperty.SetValue(formFieldsCollection, false, null);//IsReadOnly=false

            foreach (var key in formFieldsCollection.AllKeys)
            {
                var origData = formFieldsCollection[key];
                if (string.IsNullOrEmpty(origData)) continue;
                origData = origData.Trim();

                if (IgnoreList.Contains(key)) continue;
                var modifiedData = origData.ToSafeHtml();
                if (origData != modifiedData)
                {
                    //todo: log this attack...                                      
                    formFieldsCollection[key] = modifiedData;
                }
            }

            ReadonlyProperty.SetValue(formFieldsCollection, true, null);//IsReadOnly=true
        }

        private static void cleanUpAndEncodeQueryStrings(NameValueCollection queryStringsCollection)
        {
            ReadonlyProperty.SetValue(queryStringsCollection, false, null);//IsReadOnly=false

            foreach (var key in queryStringsCollection.AllKeys)
            {
                var origData = queryStringsCollection[key];
                if (string.IsNullOrEmpty(origData)) continue;
                origData = origData.Trim();

                var modifiedData = HttpUtility.HtmlEncode(CleanAllTags.Replace(origData, string.Empty));
                if (origData != modifiedData)
                {
                    //todo: log this attack...
                    queryStringsCollection[key] = modifiedData;
                }
            }

            ReadonlyProperty.SetValue(queryStringsCollection, true, null);
        }

        private static void cleanUpInput(object sender, EventArgs e)
        {
            var request = ((HttpApplication)sender).Request;

            cleanUpAndEncodeQueryStrings(request.QueryString);

            if (request.HttpMethod == "POST")
            {
                cleanUpAndEncodeFormFields(request.Form);
            }

            cleanUpAndEncodeCookies(request.Cookies);
        }

        #endregion Methods
    }
}
