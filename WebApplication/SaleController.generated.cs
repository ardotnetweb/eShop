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
    public partial class SaleController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected SaleController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult GetDataTableConfirmed()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDataTableConfirmed);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetDataTableNotConfirmedOrder()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDataTableNotConfirmedOrder);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SaleController Actions { get { return MVC.Sale; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Sale";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Sale";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ConfirmedSale = "ConfirmedSale";
            public readonly string GetDataTableNotConfirmed = "GetDataTableNotConfirmed";
            public readonly string GetDataTableConfirmed = "GetDataTableConfirmed";
            public readonly string GetDataTableNotConfirmedOrder = "GetDataTableNotConfirmedOrder";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ConfirmedSale = "ConfirmedSale";
            public const string GetDataTableNotConfirmed = "GetDataTableNotConfirmed";
            public const string GetDataTableConfirmed = "GetDataTableConfirmed";
            public const string GetDataTableNotConfirmedOrder = "GetDataTableNotConfirmedOrder";
        }


        static readonly ActionParamsClass_GetDataTableConfirmed s_params_GetDataTableConfirmed = new ActionParamsClass_GetDataTableConfirmed();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetDataTableConfirmed GetDataTableConfirmedParams { get { return s_params_GetDataTableConfirmed; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetDataTableConfirmed
        {
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_GetDataTableNotConfirmedOrder s_params_GetDataTableNotConfirmedOrder = new ActionParamsClass_GetDataTableNotConfirmedOrder();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetDataTableNotConfirmedOrder GetDataTableNotConfirmedOrderParams { get { return s_params_GetDataTableNotConfirmedOrder; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetDataTableNotConfirmedOrder
        {
            public readonly string sale_Id = "sale_Id";
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
                public readonly string _ConfirmedSale = "_ConfirmedSale";
                public readonly string _DataTableOrder = "_DataTableOrder";
                public readonly string _DataTableSaleConfirmed = "_DataTableSaleConfirmed";
                public readonly string _DataTableSaleNotConfirmed = "_DataTableSaleNotConfirmed";
            }
            public readonly string _ConfirmedSale = "~/Views/Sale/_ConfirmedSale.cshtml";
            public readonly string _DataTableOrder = "~/Views/Sale/_DataTableOrder.cshtml";
            public readonly string _DataTableSaleConfirmed = "~/Views/Sale/_DataTableSaleConfirmed.cshtml";
            public readonly string _DataTableSaleNotConfirmed = "~/Views/Sale/_DataTableSaleNotConfirmed.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_SaleController : WebApplication.Controllers.SaleController
    {
        public T4MVC_SaleController() : base(Dummy.Instance) { }

        [NonAction]
        partial void ConfirmedSaleOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ConfirmedSale()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ConfirmedSale);
            ConfirmedSaleOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetDataTableNotConfirmedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetDataTableNotConfirmed()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDataTableNotConfirmed);
            GetDataTableNotConfirmedOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetDataTableConfirmedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? page);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetDataTableConfirmed(int? page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDataTableConfirmed);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            GetDataTableConfirmedOverride(callInfo, page);
            return callInfo;
        }

        [NonAction]
        partial void GetDataTableNotConfirmedOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int sale_Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetDataTableNotConfirmedOrder(int sale_Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDataTableNotConfirmedOrder);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sale_Id", sale_Id);
            GetDataTableNotConfirmedOrderOverride(callInfo, sale_Id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
