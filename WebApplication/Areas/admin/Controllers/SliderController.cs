using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using System.IO;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using WebApplication.BaseClassWebApplication;
using System.Drawing;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    public partial class SliderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ISliderService _sliderService;
        public SliderController(IUnitOfWork unitOfWork, ISliderService sliderService)
        {
            this._unitOfWork = unitOfWork;
            this._sliderService = sliderService;
        }
        public virtual ActionResult Index()
        {
            var list = _sliderService.GetAll();
            return View(list);
        }


        public virtual ActionResult CreateSlider()
        {
            return View();
        }



        public virtual ActionResult DeleteImageSlider()
        {
            var list = _sliderService.GetAllImageForDelete();
            return View(list);
        }

        [HttpPost]
        public virtual ActionResult DeleteImageSlider(List<string> checkboxImage)
        {
            var listId = new List<Slider>();
            var listName = new List<string>();
            foreach (var item in checkboxImage)
            {
                listId.Add(new Slider { Id = int.Parse(item.Split('/')[0]) });
                listName.Add(item.Split('/')[1]);
            }
            _unitOfWork.RemoveRange<Slider>(listId);
            if (_unitOfWork.SaveAllChanges() > 0)
            {
                foreach (var item in listName)
                    TODO.DeleteImage(Server.MapPath("~/Content/Images/Slider/" + item));
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessDelete),
                    Status = AlertMode.success
                });
            }
            else
            {
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailDelete),
                    Status = AlertMode.warning
                });
            }
        }

        [HttpPost]
        public virtual ActionResult CreateSlider(ShowSliderViewModel sliderViewModel)
        {
            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                string fileName = string.Empty;
                using (var img = Image.FromStream(file.InputStream))
                {
                    fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Slider/"), file.FileName);
                    TODO.UploadImage(img, new Size(900, 350), Server.MapPath("~/Content/Images/Slider/"), fileName);
                }

                var slider = new Slider { Address = sliderViewModel.AddressSlider, Explain = sliderViewModel.ExplainSlider, Image = fileName, Title = sliderViewModel.TitleSlider };
                _sliderService.Create(slider);
                if (_unitOfWork.SaveAllChanges() > 0)
                {
                    TempData["createSlider"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessInsert), Status = AlertMode.success });
                    return RedirectToAction(actionName: MVC.admin.Slider.ActionNames.CreateSlider);
                }
                else
                    TempData["createSlider"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert), Status = AlertMode.warning });
            }

            return View();
        }
    }
}