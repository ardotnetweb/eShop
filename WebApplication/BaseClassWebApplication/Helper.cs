using eShop.WebApplication.DomainClasses.ViewModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.BaseClassWebApplication
{
    public static class Helperalert
    {
        public static MvcHtmlString alert(AlertViewModel alertmodel)
        {
            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass("row");

            TagBuilder taginner = new TagBuilder("div");
            taginner.AddCssClass("col-md-12");



            TagBuilder tagalert = new TagBuilder("div");
            tagalert.AddCssClass("alert" + " " + "alert-" + alertmodel.Status);


            TagBuilder tagbtn = new TagBuilder("button");
            tagbtn.MergeAttribute("data-dismiss", "alert");
            tagbtn.MergeAttribute("aria-hidden", "true");
            tagbtn.MergeAttribute("type", "button");
            tagbtn.AddCssClass("close");
            tagbtn.InnerHtml = ("&times;");


            TagBuilder tagStrong = new TagBuilder("strong");
            tagStrong.InnerHtml = alertmodel.Alert;



            tagalert.InnerHtml = (tagbtn.ToString(TagRenderMode.Normal));
            tagalert.InnerHtml += (tagStrong.ToString(TagRenderMode.Normal));


            taginner.InnerHtml = (tagalert.ToString(TagRenderMode.Normal));

            tag.InnerHtml = taginner.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}