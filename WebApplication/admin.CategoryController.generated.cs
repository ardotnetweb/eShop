// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
#pragma warning disable 1591, 3008, 3009
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace WebApplication.Areas.admin.Controllers
{
    public partial class CategoryController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected CategoryController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DetailsCategory()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DetailsCategory);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UpdateCategory()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCategory);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult DeleteCategory()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.DeleteCategory);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult CheckNameEdit()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.CheckNameEdit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult CheckName()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.CheckName);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetNameForAutoComplete()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetNameForAutoComplete);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public CategoryController Actions { get { return MVC.admin.Category; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Category";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Category";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string DataCategory = "DataCategory";
            public readonly string SearchCategory = "SearchCategory";
            public readonly string CreateCategory = "CreateCategory";
            public readonly string DetailsCategory = "DetailsCategory";
            public readonly string UpdateCategory = "UpdateCategory";
            public readonly string DeleteCategory = "DeleteCategory";
            public readonly string SidebarCategorySearch = "SidebarCategorySearch";
            public readonly string SearchProductByCategory = "SearchProductByCategory";
            public readonly string CheckNameEdit = "CheckNameEdit";
            public readonly string CheckName = "CheckName";
            public readonly string GetNameForAutoComplete = "GetNameForAutoComplete";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string DataCategory = "DataCategory";
            public const string SearchCategory = "SearchCategory";
            public const string CreateCategory = "CreateCategory";
            public const string DetailsCategory = "DetailsCategory";
            public const string UpdateCategory = "UpdateCategory";
            public const string DeleteCategory = "DeleteCategory";
            public const string SidebarCategorySearch = "SidebarCategorySearch";
            public const string SearchProductByCategory = "SearchProductByCategory";
            public const string CheckNameEdit = "CheckNameEdit";
            public const string CheckName = "CheckName";
            public const string GetNameForAutoComplete = "GetNameForAutoComplete";
        }


        static readonly ActionParamsClass_DataCategory s_params_DataCategory = new ActionParamsClass_DataCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DataCategory DataCategoryParams { get { return s_params_DataCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DataCategory
        {
            public readonly string term = "term";
            public readonly string count = "count";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_CreateCategory s_params_CreateCategory = new ActionParamsClass_CreateCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CreateCategory CreateCategoryParams { get { return s_params_CreateCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CreateCategory
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_DetailsCategory s_params_DetailsCategory = new ActionParamsClass_DetailsCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DetailsCategory DetailsCategoryParams { get { return s_params_DetailsCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DetailsCategory
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_UpdateCategory s_params_UpdateCategory = new ActionParamsClass_UpdateCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateCategory UpdateCategoryParams { get { return s_params_UpdateCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateCategory
        {
            public readonly string Id = "Id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_DeleteCategory s_params_DeleteCategory = new ActionParamsClass_DeleteCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteCategory DeleteCategoryParams { get { return s_params_DeleteCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteCategory
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_CheckNameEdit s_params_CheckNameEdit = new ActionParamsClass_CheckNameEdit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CheckNameEdit CheckNameEditParams { get { return s_params_CheckNameEdit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CheckNameEdit
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_CheckName s_params_CheckName = new ActionParamsClass_CheckName();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CheckName CheckNameParams { get { return s_params_CheckName; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CheckName
        {
            public readonly string name = "name";
        }
        static readonly ActionParamsClass_GetNameForAutoComplete s_params_GetNameForAutoComplete = new ActionParamsClass_GetNameForAutoComplete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetNameForAutoComplete GetNameForAutoCompleteParams { get { return s_params_GetNameForAutoComplete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetNameForAutoComplete
        {
            public readonly string term = "term";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _DataCategory = "_DataCategory";
                public readonly string _DataCategorySearch = "_DataCategorySearch";
                public readonly string _NavbarCategory_CompanySearchProduct = "_NavbarCategory_CompanySearchProduct";
                public readonly string _SearchCategory = "_SearchCategory";
                public readonly string _SidebarCategorySearch = "_SidebarCategorySearch";
                public readonly string _SolidHeaderCategory = "_SolidHeaderCategory";
                public readonly string CreateCategory = "CreateCategory";
                public readonly string DetailsCategory = "DetailsCategory";
                public readonly string Index = "Index";
                public readonly string SearchProductByCategory = "SearchProductByCategory";
                public readonly string UpdateCategory = "UpdateCategory";
            }
            public readonly string _DataCategory = "~/Areas/admin/Views/Category/_DataCategory.cshtml";
            public readonly string _DataCategorySearch = "~/Areas/admin/Views/Category/_DataCategorySearch.cshtml";
            public readonly string _NavbarCategory_CompanySearchProduct = "~/Areas/admin/Views/Category/_NavbarCategory_CompanySearchProduct.cshtml";
            public readonly string _SearchCategory = "~/Areas/admin/Views/Category/_SearchCategory.cshtml";
            public readonly string _SidebarCategorySearch = "~/Areas/admin/Views/Category/_SidebarCategorySearch.cshtml";
            public readonly string _SolidHeaderCategory = "~/Areas/admin/Views/Category/_SolidHeaderCategory.cshtml";
            public readonly string CreateCategory = "~/Areas/admin/Views/Category/CreateCategory.cshtml";
            public readonly string DetailsCategory = "~/Areas/admin/Views/Category/DetailsCategory.cshtml";
            public readonly string Index = "~/Areas/admin/Views/Category/Index.cshtml";
            public readonly string SearchProductByCategory = "~/Areas/admin/Views/Category/SearchProductByCategory.cshtml";
            public readonly string UpdateCategory = "~/Areas/admin/Views/Category/UpdateCategory.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_CategoryController : WebApplication.Areas.admin.Controllers.CategoryController
    {
        public T4MVC_CategoryController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DataCategoryOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, string term, int count, int page);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult DataCategory(string term, int count, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.DataCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "count", count);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            DataCategoryOverride(callInfo, term, count, page);
            return callInfo;
        }

        [NonAction]
        partial void SearchCategoryOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult SearchCategory()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.SearchCategory);
            SearchCategoryOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CreateCategoryOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult CreateCategory()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.CreateCategory);
            CreateCategoryOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CreateCategoryOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, WebApplication.ViewModels.CategoryAddViewModel model);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult CreateCategory(WebApplication.ViewModels.CategoryAddViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.CreateCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CreateCategoryOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void DetailsCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult DetailsCategory(int? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DetailsCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            DetailsCategoryOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void UpdateCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateCategory(int? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            UpdateCategoryOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void UpdateCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, WebApplication.ViewModels.CategoryEditViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateCategory(WebApplication.ViewModels.CategoryEditViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            UpdateCategoryOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void DeleteCategoryOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, int Id);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult DeleteCategory(int Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.DeleteCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            DeleteCategoryOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void SidebarCategorySearchOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult SidebarCategorySearch()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.SidebarCategorySearch);
            SidebarCategorySearchOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SearchProductByCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult SearchProductByCategory()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SearchProductByCategory);
            SearchProductByCategoryOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CheckNameEditOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, eShop.WebApplication.DomainClasses.MainViewModel.ExistNameCategory model);

        [NonAction]
        public override System.Web.Mvc.JsonResult CheckNameEdit(eShop.WebApplication.DomainClasses.MainViewModel.ExistNameCategory model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.CheckNameEdit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CheckNameEditOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void CheckNameOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string name);

        [NonAction]
        public override System.Web.Mvc.JsonResult CheckName(string name)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.CheckName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            CheckNameOverride(callInfo, name);
            return callInfo;
        }

        [NonAction]
        partial void GetNameForAutoCompleteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string term);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetNameForAutoComplete(string term)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetNameForAutoComplete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            GetNameForAutoCompleteOverride(callInfo, term);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
