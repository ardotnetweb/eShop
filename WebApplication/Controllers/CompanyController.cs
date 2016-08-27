using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BaseClassWebApplication;

namespace WebApplication.Controllers
{
    public partial class CompanyController : Controller
    {
        private ICategoryService _categoryService;
        private ICompanyService _companyService;
        private IProductService _productService;

        public CompanyController(ICategoryService categoryService,
                                 ICompanyService companyService,
                                 IProductService productService)
        {
            this._categoryService = categoryService;
            this._companyService = companyService;
            this._productService = productService;
        }

        public virtual ActionResult DetailsCompany(int? companyId, string companyName)
        {
            ViewBag.CompanyName = companyName;
            int id;
            if (int.TryParse(companyId.ToString(), out id))
            {
                var model = _companyService.GetCompanyById_(id);
                if (model != null)
                {
                    ViewBag.Category = new SelectList(_categoryService.GetAllRoot(), "Id", "Name");
                    ViewBag.SubCategory = new SelectList(new List<Category> { }, "Id", "Name");
                    return View(model);
                }
                else
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            else
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult AddCompanyToCart(int company_Id, string name)
        {
            var listProductCompany = _productService.GetAllProductInCompanyForAddToCart_(company_Id);
            if (Request.Cookies["Basket"] != null)
            {
                HttpCookie cookie = Request.Cookies["Basket"];
                string valueCookie = cookie["Cart"];

                //Deseralize && Decrypt
                var resultDecrypt = new EnDecryption().Decryption(valueCookie);
                var list = SerializeList.DeseraLizeList(resultDecrypt);

                list.AddRange(listProductCompany);

                //Seralize && Encrypt
                var resultSeralize = SerializeList.SeralizeList(list);
                var resultEncrypt = new EnDecryption().Encryption(resultSeralize);
                cookie.Values["Cart"] = resultEncrypt;
                cookie.Expires = DateTime.Now.AddMinutes(15);
                Response.Cookies.Add(cookie);
                return Json(new { result = "true", value = "محصول مورد نظر با موفقیت به سبد خرید اضافه شد" });
            }
            else
            {
                var resultSeralize = SerializeList.SeralizeList(listProductCompany);
                var resultEncrypt = new EnDecryption().Encryption(resultSeralize);
                HttpCookie cookie = new HttpCookie("Basket");
                cookie.Values["Cart"] = resultEncrypt;
                cookie.Expires = DateTime.Now.AddMinutes(15);
                Response.Cookies.Add(cookie);
                return Json(new { result = "true", value = "محصول مورد نظر با موفقیت به سبد خرید اضافه شد" });
            }
        }

        [ChildActionOnly]
        public virtual ActionResult InfoProductCompanySales(int id)
        {
            return PartialView(MVC.Company.Views._InforProductCompanySales, _companyService.GetInformationForAddToCart_(id));
        }

        public virtual ActionResult List()
        {
            var list = _companyService.GetAllCompanyForShow_();
            return View(list);
        }
    }
}