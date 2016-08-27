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
    public partial class CombinationController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected CombinationController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult GetAllSubCategory()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetAllSubCategory);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult GetAllCompanyByCategoryId()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetAllCompanyByCategoryId);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult productCompany()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.productCompany);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult dataCompanyProduct()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.dataCompanyProduct);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public CombinationController Actions { get { return MVC.admin.Combination; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Combination";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Combination";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string GetAllSubCategory = "GetAllSubCategory";
            public readonly string GetAllCompanyByCategoryId = "GetAllCompanyByCategoryId";
            public readonly string productCompany = "productCompany";
            public readonly string dataCompanyProduct = "dataCompanyProduct";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string GetAllSubCategory = "GetAllSubCategory";
            public const string GetAllCompanyByCategoryId = "GetAllCompanyByCategoryId";
            public const string productCompany = "productCompany";
            public const string dataCompanyProduct = "dataCompanyProduct";
        }


        static readonly ActionParamsClass_GetAllSubCategory s_params_GetAllSubCategory = new ActionParamsClass_GetAllSubCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetAllSubCategory GetAllSubCategoryParams { get { return s_params_GetAllSubCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetAllSubCategory
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_GetAllCompanyByCategoryId s_params_GetAllCompanyByCategoryId = new ActionParamsClass_GetAllCompanyByCategoryId();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetAllCompanyByCategoryId GetAllCompanyByCategoryIdParams { get { return s_params_GetAllCompanyByCategoryId; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetAllCompanyByCategoryId
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_productCompany s_params_productCompany = new ActionParamsClass_productCompany();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_productCompany productCompanyParams { get { return s_params_productCompany; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_productCompany
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_dataCompanyProduct s_params_dataCompanyProduct = new ActionParamsClass_dataCompanyProduct();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_dataCompanyProduct dataCompanyProductParams { get { return s_params_dataCompanyProduct; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_dataCompanyProduct
        {
            public readonly string Id = "Id";
            public readonly string term = "term";
            public readonly string page = "page";
            public readonly string count = "count";
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
                public readonly string _DataCompany = "_DataCompany";
                public readonly string _DataProductCategory = "_DataProductCategory";
                public readonly string _SolidHeaderCategory_SubCategory = "_SolidHeaderCategory_SubCategory";
                public readonly string _SolidHeaderProductCompany = "_SolidHeaderProductCompany";
                public readonly string Index = "Index";
                public readonly string ProductCompany = "ProductCompany";
            }
            public readonly string _DataCompany = "~/Areas/admin/Views/Combination/_DataCompany.cshtml";
            public readonly string _DataProductCategory = "~/Areas/admin/Views/Combination/_DataProductCategory.cshtml";
            public readonly string _SolidHeaderCategory_SubCategory = "~/Areas/admin/Views/Combination/_SolidHeaderCategory_SubCategory.cshtml";
            public readonly string _SolidHeaderProductCompany = "~/Areas/admin/Views/Combination/_SolidHeaderProductCompany.cshtml";
            public readonly string Index = "~/Areas/admin/Views/Combination/Index.cshtml";
            public readonly string ProductCompany = "~/Areas/admin/Views/Combination/ProductCompany.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_CombinationController : WebApplication.Areas.admin.Controllers.CombinationController
    {
        public T4MVC_CombinationController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetAllSubCategoryOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int? Id);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetAllSubCategory(int? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetAllSubCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            GetAllSubCategoryOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void GetAllCompanyByCategoryIdOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, int Id);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult GetAllCompanyByCategoryId(int Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetAllCompanyByCategoryId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            GetAllCompanyByCategoryIdOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void productCompanyOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult productCompany(int? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.productCompany);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            productCompanyOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void dataCompanyProductOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, int Id, string term, int page, int count);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult dataCompanyProduct(int Id, string term, int page, int count)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.dataCompanyProduct);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "count", count);
            dataCompanyProductOverride(callInfo, Id, term, page, count);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
