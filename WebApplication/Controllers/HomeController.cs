using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public partial class HomeController : Controller
    {
        private ICompanyService _companyservice;
        private ISliderService _sliderService;
        private IProductService _productService;
        private IPackageService _packageService;
        private ICategoryService _categoryService;
        public HomeController(ICompanyService companyservice,
                              ISliderService sliderService,
                              IProductService productService,
                              IPackageService packageService,
                              ICategoryService categoryService)
        {
            this._companyservice = companyservice;
            this._sliderService = sliderService;
            this._productService = productService;
            this._packageService = packageService;
            this._categoryService = categoryService;
        }

        public virtual ActionResult Index()
        {
            var list = _companyservice.GetAllCompanyForShow_();
            return View();
        }

        public virtual ActionResult Create()
        {
            return View();
        }

        [ChildActionOnly]
        public virtual PartialViewResult Slider()
        {
            var list = _sliderService.GetAll();
            return PartialView(MVC.Home.Views._MainSlider, list);
        }

        [ChildActionOnly]
        public virtual ActionResult SliderProduct()
        {
            var list = _productService.GetDataTableMostPopular_();
            return PartialView(MVC.Home.Views._ProductSlider, list);
        }

        [ChildActionOnly]
        public virtual PartialViewResult Package()
        {
            var list = _packageService.GetManyPackage_();
            return PartialView(MVC.Home.Views._Package, list);
        }
    }
}