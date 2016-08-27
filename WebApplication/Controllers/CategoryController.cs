using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BaseClassWebApplication;
using HtmlCleaner;

namespace WebApplication.Controllers
{
    public partial class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        private IProductService _productService;
        private ICompanyService _companyService;
        public CategoryController(ICategoryService categoryService,
                                 IProductService productService, 
                                 ICompanyService companyService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
            this._companyService = companyService;
        }
        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.Category.ActionNames.Show, MVC.Category.Name);
        }

        [HttpGet]
        public virtual ActionResult Show(int? categoryId, string categoryName)
        {
            ViewBag.CategoryName = categoryName;
            var StatusSearch = new List<SelectListItem>
            {
                new SelectListItem {Text = "پر بازدیدترین", Value = "MoreVisited",Selected = true},
                new SelectListItem {Text = "قیمت", Value = "Price"},
                new SelectListItem {Text = "جدیدترین", Value = "New"},
                new SelectListItem {Text = "پیشنهاد ویژه", Value = "Offer"}
            };
            ViewBag.StatusSearch = StatusSearch;


            var AseDes = new List<SelectListItem>
            {
                new SelectListItem {Text = "صعودی", Value = "Ascending" , Selected = true},
                new SelectListItem {Text = "نزولی", Value = "Descending"}
            };
            ViewBag.AseDes = AseDes;



            int id;
            if (int.TryParse(categoryId.ToString(), out id))
            {
                var result = _categoryService.GetById_(id);
                if (result != null)
                {
                    var model = new CategorySearchViewModel() { CategoryId = result.Id, Categories = _categoryService.GetAllSub_(result.Parent), ParentName = result.ParentName, Companies = _productService.GetDataTableNameCompany_(result.Parent) };
                    return View(model);
                }
                else
                {
                    result = _categoryService.GetRandom_();
                    var model = new CategorySearchViewModel() { CategoryId = result.Id, Categories = _categoryService.GetAllSub_(result.Parent), ParentName = result.ParentName, Companies = _productService.GetDataTableNameCompany_(result.Parent) };
                    return View(model);
                }
            }
            else
            {
                var result = _categoryService.GetRandom_();
                var model = new CategorySearchViewModel() { CategoryId = result.Id, Categories = _categoryService.GetAllSub_(result.Parent), ParentName = result.ParentName, Companies = _productService.GetDataTableNameCompany_(result.Parent) };
                return View(model);
            }
        }

        [AjaxOnly]
        public virtual ActionResult GetDataTable(string arrayCategory, string arrayCompany, string search = "", string order = "", string term = "",int count = 20, int page = 0)
        {
            int[] arrayResultCategory = (!string.IsNullOrEmpty(arrayCategory)) ? Array.ConvertAll(arrayCategory.Split(','), x => int.Parse(x)).ToArray() : null;
            int[] arrayResultCompany = (!string.IsNullOrEmpty(arrayCompany)) ? Array.ConvertAll(arrayCompany.Split(','), x => int.Parse(x)).ToArray() : null;


            var resultCategory = _categoryService.CheckCollectionId_(arrayResultCategory);
            if (resultCategory)
            {
                var resultCompany = _companyService.CheckCollectionId_(arrayResultCompany);
                if (resultCompany)
                {
                    StatusSearch serachResult = (StatusSearch)Enum.Parse(typeof(StatusSearch), search);
                    StatusOrder orderResult = (StatusOrder)Enum.Parse(typeof(StatusOrder), order);
                    //مقدار دهی
                    ViewBag.arrayCategory = arrayCategory;
                    ViewBag.search = search;
                    ViewBag.order = order;
                    ViewBag.term = term;
                    ViewBag.count = count;
                    ViewBag.page = page;
                    ViewBag.arrayCompany = arrayCompany;

                    var totalRecord = _productService.CountProductSerach_(term.ToSafeHtml(), arrayResultCategory, arrayResultCompany);
                    ViewBag.totalRecords = totalRecord;
                    ViewBag.currentPage = page + 1;
                    var list = _productService.GetdataTableProduct_(arrayResultCategory, arrayResultCompany, term.ToSafeHtml(), count, page, serachResult, orderResult);

                    if (list.Count() > 0)
                        return PartialView(viewName: "_DataTableCategory", model: list);
                    else
                        return PartialView(MVC.Shared.Views._Alert);
                }
                else
                    return PartialView(MVC.Shared.Views._Alert);
            }
            else
                return PartialView(MVC.Shared.Views._Alert);

        }


        [AjaxOnly]
        public virtual ActionResult GetAllSubCategory(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_categoryService.IsExistById(id))
                {
                    var list = _categoryService.GetAllSub(id: id);
                    return Json(list.Select(x => new DropdownSearchViewModel { Id = x.Id, Name = x.Name }),
                        JsonRequestBehavior.AllowGet);
                }
                else
                    return View();
            }
            else
                return View();
        }

    }
}