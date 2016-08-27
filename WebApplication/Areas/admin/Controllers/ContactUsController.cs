using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using System.Web.Configuration;
using WebApplication.SMSService;

namespace WebApplication.Areas.admin.Controllers
{
   
    public partial class ContactUsController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        public ContactUsController(IContactService contactService, IUnitOfWork unitOfWork,
            IApplicationUserManager userManager)
        {
            this._contactService = contactService;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        [Authorize(Roles = "private")]
        public virtual ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "private")]
        public virtual PartialViewResult DataContactUs(string term = "", int count = 20, int page = 0)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;

            var list = _contactService.GetDataTable(term, count, page);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _contactService.Count : list.Count;
            return PartialView(viewName: MVC.admin.ContactUs.Views._DataTableContactUs, model: list);
        }

        [Authorize(Roles = "private")]
        public virtual ActionResult SerachContactUs()
        {
            var ContactUscountViewModel = new ContactUsCountViewModel { Count = _contactService.Count, CountRead = _contactService.CountRead, CountUnRead = _contactService.CountUnRead };
            return PartialView(MVC.admin.ContactUs.Views._SearchContactUs, ContactUscountViewModel);
        }

        [Authorize(Roles = "private")]
        public virtual ActionResult ChageStatusRead(int id)
        {
            System.Threading.Thread.Sleep(2000);
            var model = _contactService.GetById(id);
            model.StatusRead = true;
            _contactService.Update(model);
            if (_unitOfWork.SaveAllChanges() > 0)
                return Json(new { Result = "true" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Result = "false" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "private")]
        public virtual ActionResult DeleteMessage(int id)
        {
            System.Threading.Thread.Sleep(2000);
            var model = _contactService.GetById(id);
            _contactService.Delete(model);
            if (_unitOfWork.SaveAllChanges() > 0)
                return Json(new { Result = "true" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Result = "false" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "private")]
        public virtual ActionResult AnswerContact(int user_Id, int contact_Id, string flName)
        {
            return PartialView(MVC.admin.ContactUs.Views._AnswerContact, new AnswerContactViewModel { Contact_Id = contact_Id, FLName = flName, User_Id = user_Id });
        }
        [Authorize(Roles = "private")]
        public virtual async Task<ActionResult> ResultAnswerContact(AnswerContactViewModel model)
        {
            try
            {
                var user = await _userManager.FindUserById(model.User_Id);

                string userName = WebConfigurationManager.AppSettings["UserNameSMS"];
                string password = WebConfigurationManager.AppSettings["PasswordSMS"];
                string NumberSMS = WebConfigurationManager.AppSettings["NumberSMS"];


                SendSoapClient client = new SendSoapClient();

                string result = client.SendSimpleSMS2(userName, password, user.Phone, NumberSMS, model.Message, false);

                var contact = _contactService.GetById(model.Contact_Id);
                contact.StatusAnswer = true;
                contact.StatusRead = true;

                _contactService.Update(contact);

                if (_unitOfWork.SaveAllChanges() > 0)
                    return Json(new { Result = "ارسال  پیام کوتاه با موفقیت صورت گرفت" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Result = "عدم موفقیت در ارسال  پیام کوتاه" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { Result = "عدم موفقیت در ارسال پیام کوتاه" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "private")]
        public virtual ActionResult GetFiveContactUsLat()
        {
            var model = _contactService.GetFiveLast();
            if (model.Count() > 0)
                return PartialView(MVC.admin.ContactUs.Views._DataFiveLast, model);
            else
                return PartialView(MVC.admin.ContactUs.Views._NoDataFiveLast);
        }
        [Authorize(Roles = "private,protect")]
        [HttpGet]
        public virtual ContentResult GetCountMessageNotRead()
        {
            return Content(_contactService.CountUnRead.ToString());
        }
    }
}