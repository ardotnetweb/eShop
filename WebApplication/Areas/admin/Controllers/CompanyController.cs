using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebApplication.BaseClassWebApplication;
using WebApplication.ViewModels;
using eShop.WebApplication.DomainClasses.MainViewModel;
using System.Drawing;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "protect,private")]
    public partial class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICompanyService _companyService;
        private IProductService _productService;
        public CompanyController(IUnitOfWork unitOfWork, ICompanyService companyService, IProductService productService)
        {
            this._unitOfWork = unitOfWork;
            this._companyService = companyService;
            this._productService = productService;
        }
        public virtual ViewResult Index()
        {
            return View();
        }

        public virtual ActionResult SearchCompany()
        {
            return PartialView(MVC.admin.Company.Views._SearchCompany, _companyService.Count.ToString());
        }
        public virtual PartialViewResult DataCompany(string term = "", int count = 20, int page = 0)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;

            var list = _companyService.GetDataTable(term, count, page);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ?
                _companyService.Count : list.Count;
            return PartialView(viewName: MVC.admin.Company.Views._DataCompany,
                model: list);
        }

        public virtual ActionResult DetailsCompany(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_companyService.IsExistById(id))
                    return View(model: _companyService.GetById(Id: Id));
                else
                    return RedirectToAction(actionName: MVC.admin.Company.ActionNames.Index);
            }
            else
                return RedirectToAction(actionName: MVC.admin.Company.ActionNames.Index);
        }

        [HttpGet]
        public virtual ActionResult UpdateCompany(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_companyService.IsExistById(id))
                {
                    var company = _companyService.GetById(Id: Id);
                    var model = new CompanyEditViewModel { Address = company.Address, Explain = company.Explain, ImageLogo = company.ImageLogo, Name = company.Name, Id = id };
                    return View(model: model);
                }
                else
                    return RedirectToAction(actionName: MVC.admin.Company.ActionNames.Index);
            }
            else
                return RedirectToAction(actionName: MVC.admin.Company.ActionNames.Index);
        }

        [HttpPost]
        public virtual ActionResult UpdateCompany(CompanyEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = _companyService.GetById(model.Id);
                string address = Server.MapPath("~/Content/Images/Company/");

                if (model.Image == null)
                {
                    if (company.ImageLogo != null)
                        TODO.DeleteImage(Path.Combine(address, company.ImageLogo));
                    company.ImageLogo = null;
                }
                else
                {
                    if (company.ImageLogo == null) { }
                    else
                        TODO.DeleteImage(Path.Combine(address, company.ImageLogo));

                    if (model.Image.ContentLength > 0)
                    {
                        using (var img = Image.FromStream(model.Image.InputStream))
                        {
                            string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Company/"), model.Image.FileName);
                            TODO.UploadImage(img, new Size(70, 70), Server.MapPath("~/Content/Images/Company/"), fileName);
                            company.ImageLogo = fileName;
                        }
                    }
                }

                company.Explain = model.Explain;
                company.Name = model.Name;

                if (company.Address != model.Address)
                {
                    company.Title = HelperTitle.GetTitle(Address: model.Address);
                    company.Address = model.Address;
                }
                _companyService.Update(company);
                if (_unitOfWork.SaveAllChanges() > 0)
                    return RedirectToAction(MVC.admin.Company.ActionNames.DetailsCompany, new { Id = model.Id });
                else
                {
                    TempData["updateCompany"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate), Status = AlertMode.warning });
                    return View();
                }
            }
            else
                return View(model);
        }

        [HttpGet]
        public virtual ActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult CreateCompany(CompanyAddViewModel model)
        {
            //1048576 Bytes == 1024 * 1024 KB == 1 MB ---> 1024
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Image != null)
                    {
                        if (model.Image.ContentLength > 0)
                        {
                            using (var img = Image.FromStream(model.Image.InputStream))
                            {
                                string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Company/"), model.Image.FileName);
                                TODO.UploadImage(img, new Size(70, 70), Server.MapPath("~/Content/Images/Company/"), fileName);
                                model.ImageLogo = fileName;
                            }
                        }
                    }


                    model.Title = HelperTitle.GetTitle(Address: model.Address);

                    var company = new Company { Address = model.Address, ImageLogo = model.ImageLogo, Explain = model.Explain, Name = model.Name, Title = model.Title };
                    _companyService.Create(company);
                    if (_unitOfWork.SaveAllChanges() > 0)
                        return RedirectToAction(MVC.admin.Company.ActionNames.DetailsCompany, new { Id = company.Id });
                    else
                    {
                        TempData["createCompany"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert), Status = AlertMode.warning });
                        return View();
                    }
                }
                catch
                {
                    TempData["createCompany"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert), Status = AlertMode.warning });
                    return View();
                }
            }
            else
                return View();

        }

        [HttpPost]
        public virtual PartialViewResult DelteCompany(int Id)
        {
            var company = _companyService.GetById(Id: Id);
            string address = Server.MapPath("~/Content/Images/Company/");
            if (_productService.GetByCompanyId(company.Id))
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.Dependencies),
                    Status = AlertMode.warning
                });
            }
            else
            {
                _companyService.Delete(company);
                if (_unitOfWork.SaveAllChanges() > 0)
                {
                    TODO.DeleteImage(Path.Combine(address, company.ImageLogo));

                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                    {
                        Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessDelete),
                        Status = AlertMode.success
                    });
                }
                else
                    return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                    {
                        Alert = AlertOperation.SurveyOperation(StatusOperation.FailDelete),
                        Status = AlertMode.warning
                    });
            }
        }

        public virtual JsonResult CheckName(string Name)
        {
            if (!_companyService.IsExistByName(Name))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult CheckAddress(string Address)
        {
            if (!_companyService.IsExistByAddress(Address))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult CheckNameEdit(ExistNameCompany model)
        {
            int Id = model.Id;
            var result = _companyService.GetByName(model.Name);
            if (result != null)
            {
                if (result.Id == Id)
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (result == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult CheckAddressEdit(ExistAddressCompany model)
        {
            int Id = model.Id;
            var result = _companyService.GetByAddress(model.Address);
            if (result != null)
            {
                if (result.Id == Id)
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (result == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }



        public virtual ActionResult GetNameForAutoComplete(string term)
        {
            return Json(_companyService.GetNameForAutoComplete(term), JsonRequestBehavior.AllowGet);
        }
    }
}