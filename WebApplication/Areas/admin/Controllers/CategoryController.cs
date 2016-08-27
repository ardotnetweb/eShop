using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using WebApplication.BaseClassWebApplication;
using WebApplication.ViewModels;
using eShop.WebApplication.DomainClasses.MainViewModel;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    public partial class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICategoryService _categoryService;
        private IProductService _productService;
        public CategoryController(IUnitOfWork unitOfWork,
                                  ICategoryService categoryService,
                                  IProductService productService)
        {
            this._unitOfWork = unitOfWork;
            this._categoryService = categoryService;
            this._productService = productService;
        }
        public virtual ViewResult Index()
        {
            return View();
        }
        public virtual PartialViewResult DataCategory(string term = "", int count = 20, int page = 0)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;

            var list = _categoryService.GetDataTable(term, page, count);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _categoryService.Count : list.Count;

            return PartialView(viewName: MVC.admin.Category.Views._DataCategory, model: list);
        }
        public virtual PartialViewResult SearchCategory()
        {
            return PartialView(viewName: MVC.admin.Category.Views._SearchCategory, model: _categoryService.Count.ToString());
        }
        [HttpGet]
        public virtual ViewResult CreateCategory()
        {
            ViewBag.selectParent = new SelectList(_categoryService.GetAllParentForDropdown(), "Parent_Id", "Name");
            return View();
        }
        [HttpPost]
        public virtual PartialViewResult CreateCategory(CategoryAddViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView(viewName: MVC.admin.Shared.Views._ValidationSummery);

            var category = new Category();

            if (model.Parent_Id > 0)
            {
                var rootCategory = _categoryService.GetById(Id: model.Parent_Id);
                category.Parent = rootCategory;
            }
            category.Name = model.Name;

            _categoryService.Create(category);

            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessInsert), Status = AlertMode.success });
            else
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert), Status = AlertMode.warning });
        }
        [HttpGet]
        public virtual ActionResult DetailsCategory(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_categoryService.IsExistById(id))
                    return View(model: _categoryService.GetById(Id));
                else
                    return RedirectToAction(actionName: MVC.admin.Category.ActionNames.Index,
                        controllerName: MVC.admin.Category.Name);
            }
            else
                return RedirectToAction(actionName: MVC.admin.Category.ActionNames.Index,
                    controllerName: MVC.admin.Category.Name);

        }
        [HttpGet]
        public virtual ActionResult UpdateCategory(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_categoryService.IsExistById(id))
                {
                    ViewBag.selectParent = new SelectList(_categoryService.GetAllRoot(), "Id", "Name");
                    var list = _categoryService.GetById(Id);
                    var model = new CategoryEditViewModel { Id = list.Id, Name = list.Name, Parent_Id = list.Parent.Id };
                    return View(model: model);
                }
                else
                    return RedirectToAction(actionName: MVC.admin.Category.ActionNames.Index,
                           controllerName: MVC.admin.Category.Name);
            }
            else
                return RedirectToAction(actionName: MVC.admin.Category.ActionNames.Index,
                         controllerName: MVC.admin.Category.Name);
        }
        [HttpPost]
        public virtual ActionResult UpdateCategory(CategoryEditViewModel model)
        {
            var category = new Category();
            if (model.Parent_Id > 0)
            {
                var parentCategory = _categoryService.GetById(model.Parent_Id);
                category.Parent = parentCategory;
            }
            else
                category.Parent = null;
            category.Id = model.Id;
            category.Name = model.Name;

            _categoryService.Update(category);

            if (_unitOfWork.SaveAllChanges() > 0)
                return RedirectToAction(actionName: MVC.admin.Category.ActionNames.DetailsCategory, routeValues: new { Id = model.Id });
            else
            {
                TempData["updateCategory"] = Helperalert.alert(new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.DuplicateName),
                    Status = AlertMode.info
                });
                return RedirectToAction(actionName: MVC.admin.Category.ActionNames.UpdateCategory,
                    routeValues: new { Id = model.Id });
            }
        }
        [HttpPost]
        public virtual PartialViewResult DeleteCategory(int Id)
        {

            var category = _categoryService.GetById(Id: Id);
            if (_productService.GetByCategoryId(category.Id) || _categoryService.IsExistRoot(id: category.Id))
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.Dependencies),
                    Status = AlertMode.warning
                });
            }
            else
            {
                _categoryService.Delete(category);
                if (_unitOfWork.SaveAllChanges() > 0)
                {
                    return PartialView(MVC.admin.Shared.Views._alert,
                         new AlertViewModel
                         {
                             Alert = AlertOperation.SurveyOperation
                                 (StatusOperation.SuccsessDelete),
                             Status = AlertMode.success
                         });
                }
                else
                {
                    return PartialView(MVC.admin.Shared.Views._alert,
                         new AlertViewModel
                         {
                             Alert = AlertOperation.SurveyOperation
                                 (StatusOperation.FailDelete),
                             Status = AlertMode.warning
                         });
                }
            }
        }

        public virtual PartialViewResult SidebarCategorySearch()
        {
            var result = new ClasseListTechnology();
            result.TotalProduct = _productService.Count;
            result.categories = _categoryService.GetAllCountProductPer();
            return PartialView(viewName: MVC.admin.Category.Views._SidebarCategorySearch,
                model: result);
        }

        public virtual ActionResult SearchProductByCategory()
        {
            return View();
        }

        public virtual JsonResult CheckNameEdit(ExistNameCategory model)
        {
            int Id = model.Id;
            var result = _categoryService.GetByName(model.Name);
            if (result != null)
            {
                if (result.Id == Id)
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (result == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult CheckName(string name)
        {
            if (!_categoryService.IsaExistByName(name))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }
        protected override void HandleUnknownAction(string actionName)
        {
            this.View("Index").ExecuteResult(this.ControllerContext);
        }



        public virtual ActionResult GetNameForAutoComplete(string term)
        {
            return Json(_categoryService.GetNameForAutoComplete(term), JsonRequestBehavior.AllowGet);
        }
    }
}