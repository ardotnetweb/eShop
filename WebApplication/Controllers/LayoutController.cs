using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public partial class LayoutController : Controller
    {
        private IProductService _productService;
        private ICompanyService _companyservice;
        private ICategoryService _categoryService;
        public LayoutController(IProductService productService,
                                ICompanyService companyService,
                                ICategoryService categoryService)
        {
            this._productService = productService;
            this._companyservice = companyService;
            this._categoryService = categoryService;
        }


        public virtual ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public virtual ActionResult Sidebar()
        {
            return Content("Hello Abolfazl RoshanZamir");
        }

        [ChildActionOnly]
        public virtual PartialViewResult SidebarNewProduct()
        {
            var list = _productService.GetDataTableNew_();
            return PartialView(MVC.Shared.Views._SidebarNewProduct, list);
        }


        [ChildActionOnly]
        public virtual ActionResult SidebarOfferProduct()
        {
            var list = _productService.GetdataTableProductOffer_();
            return PartialView(MVC.Shared.Views._SidebarOfferProduct, list);
        }



        [ChildActionOnly]
        public virtual PartialViewResult BottomExplainCompany()
        {
            var list = _companyservice.GetAllCompanyForShowRandom_();
            return PartialView(MVC.Shared.Views._BottomExplainCompany, list);
        }



        [ChildActionOnly]
        public virtual PartialViewResult Navbar()
        {
            var list = _categoryService.GetAllCategoryForMenu_();
            return PartialView(MVC.Shared.Views._Navbar, list);
        }

        public virtual ActionResult LoginSite()
        {
            return PartialView(MVC.Shared.Views._LoginSite);
        }
    }
}