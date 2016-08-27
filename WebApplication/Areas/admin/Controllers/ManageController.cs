using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.IdentityViewModel;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml.Linq;
using WebApplication.BaseClassWebApplication;
using WebApplication.SMSService;
using WebApplication.ViewModels;


namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private")]
    [SessionState(SessionStateBehavior.Disabled)]
    public partial class ManageController : Controller
    {

        private readonly IApplicationRoleManager _roleManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private ICityService _cityService;
        private IProvinceService _provinceService;
        private IUnitOfWork _unitOfWork;
        private ICommentService _commentService;
        private ISaleService _saleService;
        public ManageController(IApplicationUserManager userManager,
                                IApplicationRoleManager roleManager,
                                ICityService cityService,
                                IProvinceService provinceService,
                                IUnitOfWork unitOfWork,
                                ICommentService commentService,
                                ISaleService saleService,
                                IAuthenticationManager authenticationManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._cityService = cityService;
            this._provinceService = provinceService;
            this._unitOfWork = unitOfWork;
            this._commentService = commentService;
            this._saleService = saleService;
            this._authenticationManager = authenticationManager;
        }


        [Authorize(Roles = "private")]
        public virtual ActionResult Index()
        {
            var UserName = User.Identity.Name;
            var resultPrivate=User.IsInRole("private");
            var resultProtect = User.IsInRole("protect");
            var resultSample = User.IsInRole("iuydsuifsd");

            return View();
        }


        [HttpGet]
        public virtual ActionResult Register()
        {
            ViewBag.RoleId = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Register(RegisterViewModel userViewModel,
            params string[] SelectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    RegisterDate = DateTime.Now,
                    EmailConfirmed = true,
                    DateDisableUser = DateTime.Parse("1/1/1")
                };

                var adminresult = await _userManager.CreateAsync(user, userViewModel.Password);
                //Add User to the selected Roles
                if (adminresult.Succeeded)
                {
                    if (SelectedRoles != null)
                    {
                        var result = await _userManager.AddToRolesAsync(user.Id, SelectedRoles);
                        if (!result.Succeeded)
                        {
                            return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailCreateUser), Status = AlertMode.warning });

                        }
                        else
                        {
                            return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessCreateUser), Status = AlertMode.success });
                        }
                    }
                    else
                    {
                        var result = await _userManager.AddToRoleAsync(user.Id, "public");
                        if (!result.Succeeded)
                        {
                            return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailCreateUser), Status = AlertMode.warning });
                        }
                        else
                        {
                            return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessCreateUser), Status = AlertMode.success });
                        }
                    }
                }
                else
                {
                    // ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailCreateUser), Status = AlertMode.warning });
                }
            }
            else
            {
                //ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Invalid), Status = AlertMode.warning });
            }
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

        public virtual async Task<ActionResult> UpdateProfile(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var resultUser = await _userManager.FindUserById(id);
                if (resultUser != null)
                {
                    ViewBag.Provinces = new SelectList(_provinceService.GetAllInfinitProvince(), "Id", "Name", resultUser.Province_Id);
                    ViewBag.Cities = new SelectList(new List<City> { }, "Id", "Name", resultUser.City_Id);

                    var userRoles = await _userManager.GetRolesAsync(resultUser.Id);
                    resultUser.RolesList = _roleManager.Roles.ToList().Select(x => new SelectListItem()
                    {
                        Selected = userRoles.Contains(x.Name),
                        Text = x.Name,
                        Value = x.Name
                    });

                    return View(resultUser);
                }
                else
                    return RedirectToAction(MVC.admin.Manage.ActionNames.Index);
            }
            else
                return RedirectToAction(MVC.admin.Manage.ActionNames.Index);
        }


        [HttpPost]
        public virtual async Task<ActionResult> UpdateProfile(UpdateProfileUserViewModel userProfile,
            params string[] selectedRole)
        {
            var user = await _userManager.FindByIdAsync(userProfile.Id);

            AddressUser newAddress = new AddressUser();
            newAddress.City = _cityService.GetById(userProfile.City_Id);
            newAddress.Province = _provinceService.GetById(userProfile.Province_Id); ;
            newAddress.PostalCode = userProfile.PostalCode;
            newAddress.Address = userProfile.Address;


            if (user.Address != null)
            {
                user.Address.Province = newAddress.Province;
                user.Address.City = newAddress.City;
                user.Address.PostalCode = newAddress.PostalCode;
                user.Address.Address = newAddress.Address;
            }
            else
                user.Address = newAddress;


            user.PhoneNumber = userProfile.Phone;
            user.ReciveMessage = userProfile.ReciveMessage;
            user.Name = userProfile.Name;
            user.Family = userProfile.Family;


            var resultOperation = await _userManager.UpdateAsync(user);
            if (resultOperation.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user.Id);
                selectedRole = selectedRole ?? new string[] { };

                var result = await _userManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray());
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                    {
                        Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate),
                        Status = AlertMode.warning
                    });
                }
                result = await _userManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray());

                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessUpdate),
                    Status = AlertMode.success
                });
            }
            else
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate),
                    Status = AlertMode.warning
                });
            }

        }



        public virtual PartialViewResult serachUser()
        {
            var usersSystem = _userManager.Users;
            return PartialView(viewName: MVC.admin.Manage.Views._searchUser,
                model: usersSystem.Count());
        }


        public virtual PartialViewResult DataUser(string term = "", int page = 0, int count = 20)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;

            var list = _userManager.GetAllPaging(term, page, count);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _userManager.Users.Count() : list.Count;

            return PartialView(viewName: MVC.admin.Manage.Views._DataUser, model: list);

        }

        [HttpGet]
        public virtual async Task<ActionResult> CompleteInformationUser(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var userAdmin = await _userManager.FindByIdAsync(id);
                if (userAdmin != null)
                {
                    return View(model: id);
                }
                else
                    return RedirectToAction(MVC.admin.Manage.ActionNames.Index);
            }
            else
                return RedirectToAction(MVC.admin.Manage.ActionNames.Index);
        }

        [HttpGet]
        public virtual async Task<ActionResult> ProfileUser(int Id)
        {
            var userAdmin = await _userManager.FindUserById(Id);
            if (userAdmin != null)
            {
                ShowInformationUserViewModel modelUser = new ShowInformationUserViewModel();
                modelUser.ApplicationUser = userAdmin;
                modelUser.RolesUser = await _userManager.GetRolesAsync(userAdmin.Id);
                return PartialView(viewName: MVC.admin.Manage.Views._ProfileUser,
                    model: modelUser);
            }
            else
                return null;
        }



        [HttpGet]
        public virtual async Task<ActionResult> UpdateProfile(int Id)
        {
            var resultUser = await _userManager.FindUserById(Id);
            if (resultUser != null)
            {
                ViewBag.Provinces = new SelectList(_provinceService.GetAllInfinitProvince(), "Id", "Name", resultUser.Province_Id);
                ViewBag.Cities = new SelectList(new List<City> { }, "Id", "Name", resultUser.City_Id);

                var userRoles = await _userManager.GetRolesAsync(resultUser.Id);
                resultUser.RolesList = _roleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                });
                return PartialView(viewName: MVC.admin.Manage.Views._UpdateProfile,
                    model: resultUser);
            }
            else
                return null;
        }



        [HttpGet]
        public virtual ActionResult CommentUser(int Id)
        {
            var list = _commentService.GetAllPagingByUserId(count: 20, page: 0, userId: Id);
            return PartialView(viewName: MVC.admin.Manage.Views._IndexDataComment, model: list);
        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult PagedIndexComment(int? page, int Id)
        {
            var pageNumber = page ?? 0;
            var list = _commentService.GetAllPagingByUserId(count: 20, page: pageNumber, userId: Id);
            if (list == null || !list.Any())
                return Content("no-more-info");
            return PartialView(viewName: MVC.admin.Manage.Views._DataCommentUser, model: list);
        }



        public virtual ActionResult DataSaleUser(int Id)
        {
            var list = _saleService.GetAllPagingByUserId(count: 20, page: 0, userId: Id);
            return PartialView(viewName: MVC.admin.Manage.Views._IndexDataSale, model: list);
        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult PagedIndexSale(int? page, int Id)
        {
            var pageNumber = page ?? 0;
            var list = _saleService.GetAllPagingByUserId(count: 20, page: pageNumber, userId: Id);
            if (list == null || !list.Any())
                return Content("no-more-info");
            return PartialView(viewName: MVC.admin.Manage.Views._DataSale, model: list);
        }




        public async virtual Task<ActionResult> SendMessage(int Id)
        {
            var userAdmin = await _userManager.FindByIdAsync(Id);
            return PartialView(MVC.admin.Manage.Views._SendMessage, new SendSMSProfileViewModel
            {
                Email = userAdmin.Email,
                FLName = (userAdmin.Name != null && userAdmin.Family != null) ? string.Concat(userAdmin.Name, " ", userAdmin.Family) : null,
                Id = userAdmin.Id,
                Phone = (userAdmin.PhoneNumber != null) ? userAdmin.PhoneNumber : null
            });
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult SendMessage(SendSMSProfileViewModel model)
        {
            try
            {
                string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
                string password = WebConfigurationManager.AppSettings["PasswordSMS"];
                string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];

                SendSoapClient client = new SendSoapClient();
                string result = client.SendSimpleSMS2(userName, password, model.Phone, NumberSMS, model.Explain, false);
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessSend),
                    Status = AlertMode.success
                });
            }
            catch
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailSend),
                    Status = AlertMode.warning
                });
            }
        }






        [HttpGet]
        public virtual async Task<ActionResult> DisableUser(int Id)
        {
            var userAdmin = await _userManager.FindByIdAsync(Id);
            var userDisable = new DisableUserViewModel();
            userDisable.Id = userAdmin.Id;
            userDisable.Name = string.Concat(userAdmin.Name, " | ", userAdmin.Family);
            userDisable.UserName = userAdmin.UserName;
            userDisable.Status = userAdmin.LockoutEnabled;
            return PartialView(viewName: MVC.admin.Manage.Views._DisableUser,
                model: userDisable);
        }

        [HttpPost]
        public virtual async Task<ActionResult> DisableUser(DisableUserViewModel disableUser)
        {
            var userAdmin = await _userManager.FindByIdAsync(disableUser.Id);
            //userAdmin.LockoutEnabled = !disableUser.Status;
            userAdmin.DisableUser = disableUser.Status;
            var result = await _userManager.UpdateAsync(userAdmin);
            if (result.Succeeded)
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessUpdate),
                    Status = AlertMode.success
                });
            }
            else
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate),
                    Status = AlertMode.warning
                });
            }
        }


        public virtual async Task<ActionResult> DeleteUser(int Id)
        {
            var userAdmin = await _userManager.FindByIdAsync(Id);
            var userDelete = new DeleteUserProfileViewModel();
            userDelete.Id = userAdmin.Id;
            userDelete.Name = string.Concat(userAdmin.Name, " | ", userAdmin.Family);
            userDelete.UserName = userAdmin.UserName;
            userDelete.StatusActive = userAdmin.LockoutEnabled;
            return PartialView(viewName: MVC.admin.Manage.Views._DeleteUser,
                model: userDelete);
        }


        [HttpPost]
        public virtual async Task<ActionResult> DeleteUser(DeleteUserProfileViewModel deleteUser)
        {
            if (deleteUser.StatusDelete)
            {
                var userAdmin = await _userManager.FindByIdAsync(deleteUser.Id);
                var result = await _userManager.DeleteAsync(userAdmin);
                if (result.Succeeded)
                {
                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                    {
                        Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessDelete),
                        Status = AlertMode.success
                    });
                }
                else
                {
                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                    {
                        Alert = AlertOperation.SurveyOperation(StatusOperation.FailDelete),
                        Status = AlertMode.warning
                    });
                }
            }
            else
                return null;
        }


        [HttpGet]
        public virtual async Task<ActionResult> SendEmail(int Id)
        {

            var userAdmin = await _userManager.FindByIdAsync(Id);
            return PartialView(viewName: MVC.admin.Manage.Views._SendEmail, model: new SendEmailProfileViewModel
            {
                Email = userAdmin.Email,
                Explain = null,
                Family = userAdmin.Family,
                Id = userAdmin.Id,
                Name = userAdmin.Name
            });
        }



        [HttpPost]
        public virtual async Task<ActionResult> SendEmail(SendEmailProfileViewModel sendEmail)
        {
            try
            {
                string Messagedate = ConvertDate.PerssionDate(DateTime.Now);
                string Explainbody;
                using (var reader = new StreamReader(Server.MapPath("~/EmailConfiguration/TemplateEmail/_AlertModerators.txt")))
                {
                    Explainbody = reader.ReadToEnd();
                }
                string resultBody = string.Format(Explainbody, string.Concat(sendEmail.Name + " " + sendEmail.Family),
                    sendEmail.Subject, sendEmail.Explain.Replace("\r\n", "<br />"), Messagedate);
                await _userManager.SendEmailAsync(sendEmail.Id, sendEmail.Subject, resultBody);
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessSendEmail),
                    Status = AlertMode.success
                });
            }
            catch
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.FailSendEmail),
                    Status = AlertMode.warning
                });
            }
        }



        public virtual async Task<ActionResult> SendSMS()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOff()
        {
            _authenticationManager.SignOut();
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name, new { area = "" });
        }












        private string GetNameImageProfile(int Id)
        {
            XDocument document = XDocument.Load(Server.MapPath("~/App_Data/UsersImage.xml"));
            var user = document.Descendants("User")
                .Where(x => (string)x.Attribute("Id") == Id.ToString()).FirstOrDefault();

            if (user != null)
            {
                string nameImage = user.Descendants("Image").FirstOrDefault().Value;
                return nameImage;
            }
            else
                return null;
        }


        //ذخیره تصویر کاربرانی که در بخش مدیرت هستند
        private void CreateImageFileUsers(HttpPostedFileBase file, int userId)
        {
            XDocument document;
            var fileName = Path.GetFileName(file.FileName);

            if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Images/Users/"),
                fileName)))
                fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) +
                     Path.GetExtension(fileName);

            file.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/Users/"), fileName));

            if (System.IO.File.Exists(Server.MapPath("~/App_Data/UsersImage.xml")))
            {
                document = XDocument.Load(Server.MapPath("~/App_Data/UsersImage.xml"));
                document.Descendants("Users").FirstOrDefault().Add(new XElement("User",
                    new XAttribute("Id", userId),
                        new XElement("Image", fileName)));
            }
            else
            {
                document = new XDocument(new XElement("Users", new XElement("User",
                    new XAttribute("Id", userId), new XElement("Image", file.FileName))));
            }
            document.Save(Server.MapPath("~/App_Data/UsersImage.xml"));
        }
    }

}