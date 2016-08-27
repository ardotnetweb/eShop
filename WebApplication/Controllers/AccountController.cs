using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.WebApplication.DomainClasses.IdentityViewModel;
using WebApplication.ViewModels;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication.BaseClassWebApplication;
using System.IO;
using Microsoft.AspNet.Identity;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.DataLayer.EntityContext;


namespace WebApplication.Controllers
{
    public partial class AccountController : Controller
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IFavoriteUserService _favoriteUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProvinceService _provinceService;
        private readonly ICityService _cityService;
        private readonly ISaleService _saleService;
        private readonly ICommentService _commentService;
        private readonly IContactService _contactService;


        public AccountController(IApplicationUserManager userManager,
                                 IApplicationRoleManager roleManager,
                                 IApplicationSignInManager signInManager,
                                 IAuthenticationManager authenticationManager,
                                 IFavoriteUserService favoriteUserService,
                                 IUnitOfWork unitOfWork,
                                 IProvinceService provinceService,
                                 ICityService cityService,
                                 ISaleService saleService,
                                 ICommentService commentService,
                                 IContactService contactService)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._authenticationManager = authenticationManager;
            this._favoriteUserService = favoriteUserService;
            this._unitOfWork = unitOfWork;
            this._provinceService = provinceService;
            this._cityService = cityService;
            this._commentService = commentService;
            this._contactService = contactService;
            this._saleService = saleService;
        }

        public virtual ActionResult Index()
        {

            return View();
        }


        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            //if (User.Identity.IsAuthenticated)
            //    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (!user.DisableUser)
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                        switch (result)
                        {
                            case SignInStatus.Failure:
                                return Json(new { Result = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailLogin), Status = AlertMode.warning }).ToString(), Status = false }, JsonRequestBehavior.AllowGet);
                            case SignInStatus.LockedOut:
                                return null;
                            // return Json(new { Result = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.DuplicateName), Status = AlertMode.info }).ToString(), Status = false, Redirect = "Home/Index" }, JsonRequestBehavior.AllowGet);
                            case SignInStatus.RequiresVerification:
                                return null;
                            case SignInStatus.Success:
                                return Json(new { Result = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessLogin), Status = AlertMode.success }).ToString(), Status = true, Redirect = (string.IsNullOrEmpty(ReturnUrl)) ? HttpContext.Request.UrlReferrer.AbsolutePath : ReturnUrl }, JsonRequestBehavior.AllowGet);
                            //HttpContext.Request.UrlReferrer.AbsolutePath
                            default:
                                return null;
                        }
                    }
                    else
                        return Json(new { Result = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.DisableUser), Status = AlertMode.warning }).ToString(), Status = false, Redirect = "Home/Index" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { Result = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.NoExistUser), Status = AlertMode.info }).ToString(), Status = false }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Result = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Invalid), Status = AlertMode.info }).ToString(), Status = false }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOff()
        {
            _authenticationManager.SignOut();
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        private ActionResult redirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public virtual ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel_ userViewModel)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    EmailConfirmed = true,
                    RegisterDate = DateTime.Now.Date,
                    DateDisableUser = DateTime.Parse("2001/1/1")
                };

                var publicresult = await _userManager.CreateAsync(user, userViewModel.Password);
                var result = await _userManager.AddToRoleAsync(user.Id, "public");

                if (publicresult.Succeeded && result.Succeeded)
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessCreateUser), Status = AlertMode.success });
                else
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailCreateUser), Status = AlertMode.warning });
            }
            else
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.InValidCreateUser), Status = AlertMode.warning });
        }





        public virtual ActionResult RememberPassword()
        {
            return View();
        }

        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ForgotPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                var result = await _userManager.IsEmailConfirmedAsync(user.Id);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.NoExistEmail), Status = AlertMode.info });
                }

                try
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);

                    var callbackUrl = Url.Action(MVC.Account.ActionNames.ResetPassword, MVC.Account.Name,
                        new { userId = user.Id, code }, protocol: Request.Url.Scheme);


                    string Messagedate = ConvertDate.PerssionDate(DateTime.Now);
                    string Explainbody;
                    using (var reader = new StreamReader(Server.MapPath("~/EmailConfiguration/TemplateEmail/_ForgotPassword.txt")))
                    {
                        Explainbody = reader.ReadToEnd();
                    }
                    string UserName = ((user.Name != null) && (user.Family != null)) ? string.Concat(user.Name + " " + user.Family) : user.Email;
                    string resultBody = string.Format(Explainbody, UserName, "ویرایش رمز عبور", callbackUrl, Messagedate);
                    await _userManager.SendEmailAsync(user.Id, "ویرایش رمز عبور در فناوران اطلاعات ارم وب", resultBody);
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.ResetPassword), Status = AlertMode.success });
                }
                catch
                {
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailSendEmail), Status = AlertMode.warning });
                }
            }
            return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Invalid), Status = AlertMode.warning });
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string code)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.FindByNameAsync(model.Email);
            //var result=_authenticationManager
            //if (user == null)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //AddErrors(result);
            return View();
        }


        [Authorize]
        [HttpGet]
        public virtual ActionResult ChangePassword()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Invalid), Status = AlertMode.warning });


            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                    await signInAsync(user, isPersistent: false);
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessChangePassword), Status = AlertMode.success });
            }
            else
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.ChangePasswordError), Status = AlertMode.warning });

        }

        private async Task signInAsync(ApplicationUser user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
                await _userManager.GenerateUserIdentityAsync(user));
        }


        [Authorize]
        public async virtual Task<ActionResult> Management()
        {
            var _user = await _userManager.FindUserByIdForShow_(User.Identity.GetUserId<int>());
            var model = new UserViewModel
            {
                Name = _user.Name,
                Family = _user.Family,
                Phone = _user.Phone,
                Address = _user.Address,
                City = _user.City,
                Province = _user.Province,
                PostallCode = _user.PostallCode
            };
            return View(_user);
        }


        [HttpGet]
        public async virtual Task<ActionResult> CompleteInformation()
        {
            ViewBag.Province = new SelectList(_provinceService.GetAllInfinitProvince(), "Id", "Name");
            ViewBag.City = new SelectList(new List<CityShowViewModel> { }, "Id", "Name");
            var user = await _userManager.FindUserById(User.Identity.GetUserId<int>());
            return PartialView(MVC.Account.Views._CompleteInformation, user);
        }


        [AjaxOnly]
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async virtual Task<ActionResult> CompleteInformation(UpdateProfileUserViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Invalid), Status = AlertMode.warning });

            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());

            AddressUser newAddress = new AddressUser();
            newAddress.City = _cityService.GetById(model.City_Id);
            newAddress.Province = _provinceService.GetById(model.Province_Id); ;
            newAddress.PostalCode = model.PostalCode;
            newAddress.Address = model.Address;


            if (user.Address != null)
            {
                user.Address.Province = newAddress.Province;
                user.Address.City = newAddress.City;
                user.Address.PostalCode = newAddress.PostalCode;
                user.Address.Address = newAddress.Address;
            }
            else
                user.Address = newAddress;


            user.PhoneNumber = model.Phone;
            user.Name = model.Name;
            user.Family = model.Family;
            user.ReciveMessage = model.ReciveMessage;

            var resultOperation = await _userManager.UpdateAsync(user);
            if (resultOperation.Succeeded)
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessUpdate), Status = AlertMode.success });
            else
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate), Status = AlertMode.warning });


        }

        [AjaxOnly]
        public virtual JsonResult GetAllCityById(int province_Id)
        {
            var list = _cityService.GetAllCityByProvinceId(province_Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public async virtual Task<ActionResult> CheckFullInformation()
        {
            var _user = await _userManager.FindUserByIdForShow_(User.Identity.GetUserId<int>());
            if (_user.Address != null && _user.City != null && _user.Province != null && _user.PostallCode != null
                && _user.Phone != null && _user.Name != null && _user.Family != null)
                return Json(new { Status = "true", Address = "/cart/confirmation" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Status = "false", Address = HttpContext.Request.UrlReferrer.AbsolutePath }, JsonRequestBehavior.AllowGet);
        }


        [AjaxOnly]
        public virtual async Task<ActionResult> GetBaseInfoUser()
        {
            var _user = await _userManager.FindUserByIdForShow_(User.Identity.GetUserId<int>());

            var model = new UserViewModel
            {
                Name = _user.Name,
                Family = _user.Family,
                Phone = _user.Phone,
                Address = _user.Address,
                City = _user.City,
                Province = _user.Province,
                PostallCode = _user.PostallCode,
                ReciveMessage = _user.ReciveMessage
            };
            return PartialView(MVC.Account.Views._BaseInfoUser, _user);
        }

        public async Task<JsonResult> CheckEmail(string Email)
        {
            var nateja = await _userManager.FindByNameAsync(Email);
            var result = await _userManager.FindByEmailAsync(Email);
            if (result != null)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }


        [AjaxOnly]
        [HttpGet]
        public virtual ActionResult GetCountInfoUser()
        {
            int userId = User.Identity.GetUserId<int>();
            var countUserInfo = new CountInfoUser();
            countUserInfo.CountComment = _commentService.CountCommentUser(userId);
            countUserInfo.CountMessage = _contactService.CountMessageUser(userId);

            countUserInfo.CountOrder = _saleService.CountSaleUser(userId);


            countUserInfo.CountNotConfirmed = _saleService.CountNotConfirmedUser(userId);
            countUserInfo.CountProcess = _saleService.CountProcessUser(userId);
            countUserInfo.CountSend = _saleService.CountSendUser(userId);
            countUserInfo.CountFavorite = _favoriteUserService.CountFavoriteUser(userId);
            return PartialView(MVC.Account.Views._BaseTotalInfo, countUserInfo);
        }

    }
}