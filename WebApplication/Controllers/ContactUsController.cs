using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.UI;
using WebApplicationCapchaImagePersian.Models;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.DataLayer.EntityContext;
using HtmlCleaner;

namespace WebApplication.Controllers
{
    public partial class ContactUsController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IContactService _contactService;
        private readonly IUnitOfWork _unitOfWork;
        public ContactUsController(IApplicationUserManager userManager,
            IContactService contactService, IUnitOfWork unitOfWork)
        {
            this._userManager = userManager;
            this._contactService = contactService;
            this._unitOfWork = unitOfWork;
        }

        [NoBrowserCache]
        public async virtual Task<ActionResult> Index()
        {
            var model = new ContactUsViewModel { };
            var _user = await _userManager.FindUserByIdForShow_(User.Identity.GetUserId<int>());
            if (_user != null)
            {
                if (_user.Name != null && _user.Family != null && _user.Phone != null)
                {
                    model.Family = _user.Family;
                    model.Phone = _user.Phone;
                    model.Name = _user.Name;
                }
            }
            return View(model);
        }
        [HttpPost, ValidateCaptchaAttribute, ValidateAntiForgeryToken]
        public async virtual Task<ActionResult> Index(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user.Name == null || user.Family == null || user.PhoneNumber == null)
                {
                    user.PhoneNumber = model.Phone;
                    user.Name = model.Name;
                    user.Family = model.Family;
                }
                var resultOperation = await _userManager.UpdateAsync(user);
                if (resultOperation.Succeeded)
                {
                    _contactService.Create(new ContactUs { ApplicationUser = user, Date = DateTime.Now, Explain = model.Explain.ToSafeHtml(), StatusRead = false, StatusAnswer = false });
                    if (_unitOfWork.SaveAllChanges() > 0)
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessSend), Status = AlertMode.success });
                    else
                        return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailSend), Status = AlertMode.warning });
                }
                else
                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailSend), Status = AlertMode.warning });
            }
            else
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Invalid), Status = AlertMode.warning });
        }

        [NoBrowserCache]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "None")]
        public virtual CaptchaImageResult CaptchaImage(string rndDate)
        {
            return new CaptchaImageResult();
        }

    }
}