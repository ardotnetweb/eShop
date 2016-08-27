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
using WebApplication.BaseClassWebApplication;
using WebApplication.ViewModels;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    [SessionState(SessionStateBehavior.Disabled)]
    public partial class ProvinceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IProvinceService _provinceService;
        private ICityService _cityService;
        public ProvinceController(IUnitOfWork unitOfWork,
                                  IProvinceService provinceService,
                                  ICityService cityService)
        {
            this._unitOfWork = unitOfWork;
            this._provinceService = provinceService;
            this._cityService = cityService;
        }
        public virtual ActionResult Index()
        {
            var provinces = _provinceService.GetAll();
            return View(provinces);
        }

        public virtual PartialViewResult SearchProvince()
        {
            var countProvince = _provinceService.Count;
            return PartialView(viewName: MVC.admin.Province.Views._searchprovince,
                               model: countProvince);

        }

        [HttpGet]
        public virtual JsonResult serachDataProvince(string term)
        {
            var list = _provinceService.GetAllProvinceByName(term);
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public virtual ActionResult UpdateProvince(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_provinceService.IsExistById(id))
                    return View(_provinceService.GetById(Id));
                else
                    return RedirectToAction(MVC.admin.Province.ActionNames.Index);
            }
            else
                return RedirectToAction(MVC.admin.Province.ActionNames.Index);

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult UpdateProvince(Province province)
        {
            _provinceService.Update(province);
            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessUpdate), Status = AlertMode.success });
            else
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate), Status = AlertMode.warning });
        }



        [HttpPost]
        public virtual PartialViewResult DeleteProvince(Province province)
        {
            try
            {
                if (_provinceService.IsExistById(province.Id))
                {
                    var cities = _cityService.GetAllCitiesByProvinceId(province.Id);
                    _unitOfWork.RemoveRange<City>(cities);
                    _provinceService.Delete(_provinceService.GetById(province.Id));
                    if (_unitOfWork.SaveAllChanges() > 0)
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessDelete), Status = AlertMode.success });
                    else
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailDelete), Status = AlertMode.warning });
                }
                else
                    //باید خطا این باشد : چنین رکوردی موجود نیست
                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailDelete), Status = AlertMode.warning });
            }
            catch
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Dependencies), Status = AlertMode.info });
            }
        }



        public virtual ActionResult CreateProvince()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult CreateProvince(ProvinceAddViewModel provinceViewModel)
        {
            var province = new Province { Name = provinceViewModel.Name };
            _provinceService.Create(province);
            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessInsert), Status = AlertMode.success });
            else
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert), Status = AlertMode.warning });
        }

        public virtual JsonResult CheckName(string Name)
        {
            if (!_provinceService.IsExistByName(Name))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}