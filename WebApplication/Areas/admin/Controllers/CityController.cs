using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using WebApplication.ViewModels;
using System.Web.SessionState;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    [SessionState(SessionStateBehavior.Disabled)]
    public partial class CityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IProvinceService _provinceService;
        private ICityService _cityService;

        public CityController(IProvinceService provinceService,
                                  ICityService cityService,
                                  IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._provinceService = provinceService;
            this._cityService = cityService;
        }

        public virtual ActionResult Index()
        {
            ViewBag.Provinces = new SelectList(_provinceService.GetAllInfinitProvince(), "Id", "Name");
            return View();
        }

        public virtual JsonResult GetCityByIdProvince(int Id)
        {
            var list = _cityService.GetAllCityByProvinceId(Id);
            return Json(data: list, behavior: JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult UpdateCity(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_cityService.IsExistCityById(id))
                {
                    var city = _cityService.GetById(id);
                    ViewBag.Provinces = new SelectList(_provinceService.GetAllInfinitProvince(), "Id", "Name", city.Province.Id);
                    var resultCity = _cityService.GetById(id);
                    return View(resultCity);
                }
                else
                    return RedirectToAction(MVC.admin.City.ActionNames.Index);
            }
            return RedirectToAction(MVC.admin.City.ActionNames.Index);
        }

        [HttpPost]
        public virtual ActionResult UpdateCity(City city)
        {
            City newCity = new City();
            newCity.Name = city.Name;
            newCity.Id = city.Id;
            newCity.Province = _provinceService.GetById(city.Province.Id);
            _cityService.Update(newCity);
            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessUpdate), Status = AlertMode.success });
            else
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate), Status = AlertMode.warning });
    
        }

        [HttpPost]
        public virtual PartialViewResult DeleteCity(int Id)
        {
            try
            {
                if (_cityService.IsExistCityById(Id))
                {
                    _cityService.Delete(_cityService.GetById(Id));
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

        [HttpGet]
        public virtual ActionResult CreateCity()
        {
            ViewBag.Provinces = new SelectList(_provinceService.GetAllInfinitProvince(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public virtual ActionResult CreateCity(CityAddViewModel cityViewModel)
        {
            var city = new City() { Name = cityViewModel.Name, Province = _provinceService.GetById(cityViewModel.ProvinceId) };
            _cityService.Create(city);
            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessInsert), Status = AlertMode.success });
            else
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert), Status = AlertMode.warning });
        }
        public virtual JsonResult CheckName(string Name)
        {
            if (!_cityService.IsExistVityByName(Name))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}