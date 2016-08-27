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
namespace WebApplication.Controllers
{
    public partial class ProductController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProductController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Search()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Search);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DetailSProduct()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DetailSProduct);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SimilarProduct()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SimilarProduct);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AddToCart()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddToCart);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Add()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Add);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProductController Actions { get { return MVC.Product; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Product";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Product";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Search = "Search";
            public readonly string DetailSProduct = "DetailSProduct";
            public readonly string SimilarProduct = "SimilarProduct";
            public readonly string GetAllProductCompanyPerCategory = "GetAllProductCompanyPerCategory";
            public readonly string AddToCart = "AddToCart";
            public readonly string Add = "Add";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Search = "Search";
            public const string DetailSProduct = "DetailSProduct";
            public const string SimilarProduct = "SimilarProduct";
            public const string GetAllProductCompanyPerCategory = "GetAllProductCompanyPerCategory";
            public const string AddToCart = "AddToCart";
            public const string Add = "Add";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string categoryId = "categoryId";
            public readonly string categoryName = "categoryName";
        }
        static readonly ActionParamsClass_Search s_params_Search = new ActionParamsClass_Search();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Search SearchParams { get { return s_params_Search; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Search
        {
            public readonly string array = "array";
            public readonly string term = "term";
            public readonly string search = "search";
            public readonly string order = "order";
        }
        static readonly ActionParamsClass_DetailSProduct s_params_DetailSProduct = new ActionParamsClass_DetailSProduct();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DetailSProduct DetailSProductParams { get { return s_params_DetailSProduct; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DetailSProduct
        {
            public readonly string productId = "productId";
            public readonly string productName = "productName";
        }
        static readonly ActionParamsClass_SimilarProduct s_params_SimilarProduct = new ActionParamsClass_SimilarProduct();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SimilarProduct SimilarProductParams { get { return s_params_SimilarProduct; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SimilarProduct
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_GetAllProductCompanyPerCategory s_params_GetAllProductCompanyPerCategory = new ActionParamsClass_GetAllProductCompanyPerCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetAllProductCompanyPerCategory GetAllProductCompanyPerCategoryParams { get { return s_params_GetAllProductCompanyPerCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetAllProductCompanyPerCategory
        {
            public readonly string page = "page";
            public readonly string category_Id = "category_Id";
            public readonly string company_Id = "company_Id";
        }
        static readonly ActionParamsClass_AddToCart s_params_AddToCart = new ActionParamsClass_AddToCart();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddToCart AddToCartParams { get { return s_params_AddToCart; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddToCart
        {
            public readonly string product_Id = "product_Id";
            public readonly string name = "name";
            public readonly string price = "price";
            public readonly string quantity = "quantity";
            public readonly string type = "type";
        }
        static readonly ActionParamsClass_Add s_params_Add = new ActionParamsClass_Add();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Add AddParams { get { return s_params_Add; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Add
        {
            public readonly string product_Id = "product_Id";
            public readonly string name = "name";
            public readonly string price = "price";
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
                public readonly string _BaseInformation = "_BaseInformation";
                public readonly string _MainSlider = "_MainSlider";
                public readonly string _ProductPerPackage = "_ProductPerPackage";
                public readonly string _SimilarSlider = "_SimilarSlider";
                public readonly string DetailsProduct = "DetailsProduct";
            }
            public readonly string _BaseInformation = "~/Views/Product/_BaseInformation.cshtml";
            public readonly string _MainSlider = "~/Views/Product/_MainSlider.cshtml";
            public readonly string _ProductPerPackage = "~/Views/Product/_ProductPerPackage.cshtml";
            public readonly string _SimilarSlider = "~/Views/Product/_SimilarSlider.cshtml";
            public readonly string DetailsProduct = "~/Views/Product/DetailsProduct.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProductController : WebApplication.Controllers.ProductController
    {
        public T4MVC_ProductController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? categoryId, string categoryName);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(int? categoryId, string categoryName)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryId", categoryId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryName", categoryName);
            IndexOverride(callInfo, categoryId, categoryName);
            return callInfo;
        }

        [NonAction]
        partial void SearchOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string array, string term, string search, string order);

        [NonAction]
        public override System.Web.Mvc.ActionResult Search(string array, string term, string search, string order)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Search);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "array", array);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "search", search);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "order", order);
            SearchOverride(callInfo, array, term, search, order);
            return callInfo;
        }

        [NonAction]
        partial void DetailSProductOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? productId, string productName);

        [NonAction]
        public override System.Web.Mvc.ActionResult DetailSProduct(int? productId, string productName)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DetailSProduct);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productName", productName);
            DetailSProductOverride(callInfo, productId, productName);
            return callInfo;
        }

        [NonAction]
        partial void SimilarProductOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult SimilarProduct(int Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SimilarProduct);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            SimilarProductOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void GetAllProductCompanyPerCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? page, int? category_Id, int? company_Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetAllProductCompanyPerCategory(int? page, int? category_Id, int? company_Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetAllProductCompanyPerCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "category_Id", category_Id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "company_Id", company_Id);
            GetAllProductCompanyPerCategoryOverride(callInfo, page, category_Id, company_Id);
            return callInfo;
        }

        [NonAction]
        partial void AddToCartOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int product_Id, string name, double price, int quantity, eShop.WebApplication.DomainClasses.AppClasses.StatusTypeOrder type);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddToCart(int product_Id, string name, double price, int quantity, eShop.WebApplication.DomainClasses.AppClasses.StatusTypeOrder type)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddToCart);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "product_Id", product_Id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "price", price);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "quantity", quantity);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "type", type);
            AddToCartOverride(callInfo, product_Id, name, price, quantity, type);
            return callInfo;
        }

        [NonAction]
        partial void AddOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int product_Id, string name, double price);

        [NonAction]
        public override System.Web.Mvc.ActionResult Add(int product_Id, string name, double price)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Add);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "product_Id", product_Id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "price", price);
            AddOverride(callInfo, product_Id, name, price);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
