using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BaseClassWebApplication;

namespace WebApplication.Controllers
{
    public partial class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult DetailsPackage(int? packageId, string packageName)
        {
            ViewBag.NamePackage = packageName;
            int id;
            if (int.TryParse(packageId.ToString(), out id))
            {
                var package = _packageService.GetPackageByIdPackage(id);
                if (package != null)
                    return View(model: package);
                else
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            }
            else
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        [AjaxOnly]
        public virtual ActionResult LoadProductPerPackage(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var list = _packageService.GetProductPerPackage(id: id);
                return PartialView(MVC.Product.Views._ProductPerPackage,list);
            }
            else
                return Content("");
        }
    }
}