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
    public partial class BlockController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected BlockController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult DeleteIPBlock()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.DeleteIPBlock);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetIPForAutoComplete()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetIPForAutoComplete);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BlockController Actions { get { return MVC.admin.Block; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Block";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Block";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string DataIPBlock = "DataIPBlock";
            public readonly string SerachIPBlock = "SerachIPBlock";
            public readonly string DeleteIPBlock = "DeleteIPBlock";
            public readonly string GetIPForAutoComplete = "GetIPForAutoComplete";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string DataIPBlock = "DataIPBlock";
            public const string SerachIPBlock = "SerachIPBlock";
            public const string DeleteIPBlock = "DeleteIPBlock";
            public const string GetIPForAutoComplete = "GetIPForAutoComplete";
        }


        static readonly ActionParamsClass_DataIPBlock s_params_DataIPBlock = new ActionParamsClass_DataIPBlock();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DataIPBlock DataIPBlockParams { get { return s_params_DataIPBlock; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DataIPBlock
        {
            public readonly string term = "term";
            public readonly string count = "count";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_DeleteIPBlock s_params_DeleteIPBlock = new ActionParamsClass_DeleteIPBlock();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteIPBlock DeleteIPBlockParams { get { return s_params_DeleteIPBlock; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteIPBlock
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_GetIPForAutoComplete s_params_GetIPForAutoComplete = new ActionParamsClass_GetIPForAutoComplete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetIPForAutoComplete GetIPForAutoCompleteParams { get { return s_params_GetIPForAutoComplete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetIPForAutoComplete
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
                public readonly string _DataIPBlock = "_DataIPBlock";
                public readonly string _SearchIPBlock = "_SearchIPBlock";
                public readonly string _SolidHeaderIPBlock = "_SolidHeaderIPBlock";
                public readonly string Index = "Index";
            }
            public readonly string _DataIPBlock = "~/Areas/admin/Views/Block/_DataIPBlock.cshtml";
            public readonly string _SearchIPBlock = "~/Areas/admin/Views/Block/_SearchIPBlock.cshtml";
            public readonly string _SolidHeaderIPBlock = "~/Areas/admin/Views/Block/_SolidHeaderIPBlock.cshtml";
            public readonly string Index = "~/Areas/admin/Views/Block/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_BlockController : WebApplication.Areas.admin.Controllers.BlockController
    {
        public T4MVC_BlockController() : base(Dummy.Instance) { }

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
        partial void DataIPBlockOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string term, int count, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult DataIPBlock(string term, int count, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DataIPBlock);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "count", count);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            DataIPBlockOverride(callInfo, term, count, page);
            return callInfo;
        }

        [NonAction]
        partial void SerachIPBlockOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult SerachIPBlock()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.SerachIPBlock);
            SerachIPBlockOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DeleteIPBlockOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int Id);

        [NonAction]
        public override System.Web.Mvc.JsonResult DeleteIPBlock(int Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.DeleteIPBlock);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            DeleteIPBlockOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void GetIPForAutoCompleteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string term);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetIPForAutoComplete(string term)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetIPForAutoComplete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            GetIPForAutoCompleteOverride(callInfo, term);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
