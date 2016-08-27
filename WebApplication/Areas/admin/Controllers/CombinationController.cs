using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    public partial class CombinationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private ICompanyService _companyService;
        public CombinationController(IUnitOfWork unitOfWork, IProductService productService,
            ICategoryService categoryService, ICompanyService companyService)
        {
            this._unitOfWork = unitOfWork;
            this._categoryService = categoryService;
            this._productService = productService;
            this._companyService = companyService;
        }


        [HttpGet]
        public virtual ActionResult Index()
        {
            ViewBag.Category = new SelectList(_categoryService.GetAllRoot(), "Id", "Name");
            ViewBag.SubCategory = new SelectList(new List<Category> { }, "Id", "Name");
            return View();
        }
        public virtual JsonResult GetAllSubCategory(int? Id)
        {
            if (Id != null)
            {
                var id = Id ?? 0;
                var list = _categoryService.GetAllSub(id:id);
                return Json(list.Select(x => new DropdownSearchViewModel { Id = x.Id, Name = x.Name }),
                    JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new DropdownSearchViewModel { }, JsonRequestBehavior.AllowGet);
        }
        public virtual PartialViewResult GetAllCompanyByCategoryId(int Id)
        {
            return PartialView(viewName: MVC.admin.Combination.Views._DataCompany,
                model: _productService.GetAllCompanyWithCategoryId(Id));
        }
        [HttpGet]
        public virtual ActionResult productCompany(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_companyService.IsExistById(id))
                    return View(Id);
                else
                    return RedirectToAction(actionName: MVC.admin.Combination.ActionNames.Index, controllerName: MVC.admin.Combination.Name);
            }
            else
                return RedirectToAction(actionName: MVC.admin.Combination.ActionNames.Index, controllerName: MVC.admin.Combination.Name);
        }
        public virtual PartialViewResult dataCompanyProduct(int Id, string term = "", int page = 0, int count = 20)
        {
            ViewBag.Id = Id;
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;
            var list = _productService.GetDataTablePerCompany(Id, term, count, page);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _productService.CountProductPerCompany(id: Id) : list.Count;
            return PartialView(viewName: MVC.admin.Combination.Views._DataProductCategory, model: list);
        }

    }
}
