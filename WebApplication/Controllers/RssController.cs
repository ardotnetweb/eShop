using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.RssApplication;
using WebApplication.SitemapApplication;

namespace WebApplication.Controllers
{
    public partial class RssController : Controller
    {
        private readonly IProductService _productService;
        public RssController(IProductService productService)
        {
            this._productService = productService;
        }
        [HttpGet]
        public virtual ActionResult Index()
        {
            var list = _productService.GetLstItemsRss();
            RSS rss = new RSS();

            if (list.Count() > 0)
            {
                rss.Chanel = new Chanel()
                {
                    Title = "فید آخرین محصولات سایت",
                    Description = "برنامه نویسی وب - طراحی وب - پایگاه داده - برنامه نویسی ویندوز فون",
                    Language = "fa-IR",
                    Link = Request.Url.GetLeftPart(UriPartial.Authority),
                    LastBuildDate = new RSS().CalculateDate(list[0].Date)
                };
                var listContentRss = new List<ContentRss>();
                foreach (var item in list)
                {
                    listContentRss.Add(new ContentRss(item.Date)
                    {
                        Description = item.Explain.ToString(),
                        Link = string.Format("{0}/product/detailsproduct/{1}/{2}",
                        Request.Url.GetLeftPart(UriPartial.Authority), item.Product_Id, item.Product_Name.Replace(' ', '-')),
                        Title = item.Product_Name
                    });
                }
                rss.Chanel.Item = listContentRss.ToArray();
            }
            return new XmlResult(rss);
        }

        public virtual ActionResult Tag(string name)
        {
            var list = _productService.GetLstItemsCategoryRss(name);
            RSS rss = new RSS();

            if (list.Count() > 0)
            {
                rss.Chanel = new Chanel()
                {
                    Title = string.Format("فید آخریمن محصولات گروه {0}", name),
                    Description = string.Format("گروه محصولات {0}", name),
                    Language = "fa-IR",
                    Link = Request.Url.GetLeftPart(UriPartial.Authority),
                    LastBuildDate = new RSS().CalculateDate(list[0].Date)
                };
                var listContentRss = new List<ContentRss>();
                foreach (var item in list)
                {
                    listContentRss.Add(new ContentRss(item.Date)
                    {
                        Description = item.Explain.ToString(),
                        Link = string.Format("{0}/product/detailsproduct/{1}/{2}",
                        Request.Url.GetLeftPart(UriPartial.Authority), item.Product_Id, item.Product_Name.Replace(' ', '-')),
                        Title = item.Product_Name
                    });
                }
                rss.Chanel.Item = listContentRss.ToArray();
            }
            return new XmlResult(rss);
        }
    }
}