using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    [SessionState(SessionStateBehavior.Disabled)]
    public partial class LabelController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private ILabelService _labelService;

        public LabelController(IUnitOfWork unitOfWork, ILabelService labelService)
        {
            this._unitOfWork = unitOfWork;
            this._labelService = labelService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Searchlabel()
        {
            return PartialView(MVC.admin.Label.Views._SearchLabel, _labelService.Count.ToString());
        }


        public virtual PartialViewResult DataLabel(string term = "", int count = 20, int page = 0)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;

            var list = _labelService.GetDataTable(term, count, page);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ?
                _labelService.Count : list.Count;
            return PartialView(viewName: MVC.admin.Label.Views._DataLabel,
                model: list);
        }

        [HttpPost]
        public virtual PartialViewResult CreateLabel(Label label)
        {

            if (_labelService.IsExistLabel(label.Name))
            {
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.DuplicateName), Status = AlertMode.info });
            }
            else
            {
                _labelService.Create(label);
                if (_unitOfWork.SaveAllChanges() > 0)
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessInsert), Status = AlertMode.success });
                else
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert), Status = AlertMode.warning });
            }

        }

        [HttpGet]
        public virtual ActionResult CreateLabel()
        {
            return PartialView();
        }


        [HttpPost]
        public virtual PartialViewResult DeleteLabel(int Id)
        {
            _labelService.Delete(_labelService.GetById(Id: Id));
            if (_unitOfWork.SaveAllChanges() > 0)
            {
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessInsert),
                    Status = AlertMode.success
                });
            }
            else
            {
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert),
                    Status = AlertMode.warning
                });
            }
        }


        public virtual PartialViewResult DetailsLabel(int Id)
        {
            var label = _labelService.GetById(Id: Id);
            return PartialView(viewName: MVC.admin.Label.Views._DetailsLabel, model: label);
        }


        public virtual ActionResult GetNameForAutoComplete(string term)
        {
            return Json(_labelService.GetNameForAutoComplete(term), JsonRequestBehavior.AllowGet);
        }

    }
}