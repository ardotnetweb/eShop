using eShop.WebApplication.DomainClasses.ViewModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication.BaseClassWebApplication;
using WebApplication.Lucene;

namespace WebApplication.Controllers
{
    public partial class SearchController : Controller
    {
        public virtual ActionResult Index(string term)
        {
            ViewBag.Term = term;
            IEnumerable<LuceneProductModel> model = LuceneProductSearch.Search(term, "Product_Name", "Product_Explain").Take(100);
            const string highlightPattern = @"<b style='color:green;'>$1</b>";



            foreach (LuceneProductModel product in model)
            {
                product.Product_Name = Regex.Replace(product.Product_Name ?? " ", string.Format("({0})", term), highlightPattern, RegexOptions.IgnoreCase);
                product.Product_Explain = Regex.Replace(product.Product_Explain ?? " ", string.Format("({0})", term), highlightPattern, RegexOptions.IgnoreCase);
            }

            return View(model);
        }


        public virtual ActionResult AutoCompleteSearch(string term)
        {
            IEnumerable<LuceneProductModel> model = LuceneProductSearch.Search(term, "Product_Name", "Product_Explain").Take(10);
            var list = model.Select(x => x.Product_Name).ToList();


            var data =
                    model.Select(x => new
                    {
                        label = x.Product_Name,
                        url = Url.RouteUrl("DetailsProduct", new { productId = x.Product_Id, productName = UrlExtensions.ResolveTitleForUrl(x.Product_Name) })
                    });

            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}