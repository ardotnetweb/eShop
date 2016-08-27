using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using WebApplication.BaseClassWebApplication;


namespace WebApplication.Areas.admin.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [Authorize(Roles = "private,protect")]

    public partial class HomeController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IContactService _contactService;
        private readonly ISaleService _saleService;
        private readonly IApplicationUserManager _userManager;
        private readonly IPackageService _packageService;
        private readonly ILabelService _labelService;
        private readonly IProductService _productService;
        public HomeController(ICommentService _commentService,
                              IContactService _contactService,
                              ISaleService _saleService,
                              IApplicationUserManager _userManager,
                              IPackageService _packageService,
                              ILabelService _labelService,
                              IProductService _productService)
        {
            this._commentService = _commentService;
            this._contactService = _contactService;
            this._userManager = _userManager;
            this._saleService = _saleService;
            this._packageService = _packageService;
            this._labelService = _labelService;
            this._productService = _productService;
        }


        [HttpGet]
        public virtual ActionResult GetCount()
        {
            var model = new CountSite();
            model.ContSaleNotConfirmed = _saleService.CountNotConfirmed;
            model.CountCommentNotRead = _commentService.CountUnRead;
            model.CountMessageNotRead = _contactService.CountUnRead;
            model.CountLabels = _labelService.Count;
            model.CountOfferProduct = _productService.CountOffer;
            model.CountActivePackage = _packageService.CountActive;
            model.CountDisablePackage = _packageService.CountDisable;
            model.CountUsers = 0;

            return PartialView(MVC.admin.Home.Views._CountSite, model);
        }

        //[CustomAuthorizeAttribute]
        //[CustomAuthorize("private,protect")]
      //  [Authorize(Roles = "private,protect")]
        public virtual ActionResult index()
        {
            return View();
        }


        [HttpGet]
        public virtual ActionResult GetDate()
        {
            return Content(WebApplication.BaseClassWebApplication.ConvertDateChart.ConvertToPerssionDay(DateTime.Now).Replace('/', ' '));
        }



    }
}
