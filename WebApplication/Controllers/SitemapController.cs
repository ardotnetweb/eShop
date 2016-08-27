using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.SitemapApplication;

namespace WebApplication.Controllers
{
    public partial class SitemapController : Controller
    {
        private readonly IProductService _productService;
        public SitemapController(IProductService productService)
        {
            this._productService = productService;
        }
        [HttpGet]
        public virtual ActionResult Index()
        {
            var list = _productService.GetLstItems();
            SitemapModel model = new SitemapModel();
            if(list.Count() >0)
            {
                model.Add(new Location
                {
                    Url = Request.Url.GetLeftPart(UriPartial.Authority),
                    Priority = Priority.priority_10,
                    LastModified = list[0].ModifiedDate.ToString("yyyy/MM/dd"),
                    ChangeFrequency = eChangeFrequency.weekly
                });
                foreach (var item in list)
                {
                    model.Add(new Location
                    {
                        ChangeFrequency = item.ChangeFrequency,
                        LastModified = item.ModifiedDate.ToString("yyyy/MM/dd"),
                        Priority = (double)item.Priority,
                        Url = string.Format("{0}/product/detailsproduct/{1}/{2}",
                        Request.Url.GetLeftPart(UriPartial.Authority), item.Product_Id, item.Product_Name.Replace(' ', '-'))
                    });
                }

            }
            return new XmlResult(model);
        }
        protected override void HandleUnknownAction(string actionName)
        {
            switch (actionName)
            {
                case "":
                    break;
                default:
                    break;
            }
            this.RedirectToAction("Index").ExecuteResult(this.ControllerContext);
        }
    }
}