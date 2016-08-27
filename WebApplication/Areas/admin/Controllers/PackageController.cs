using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.MainViewModel;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication.BaseClassWebApplication;
using WebApplication.ViewModels;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    public partial class PackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICategoryService _categoryService;
        private IProductService _productService;
        private IPackageService _packageService;
        public PackageController(IUnitOfWork unitOfWork,
                                ICategoryService categoryService,
                                IProductService productService,
                                IPackageService packageService)
        {
            this._unitOfWork = unitOfWork;
            this._productService = productService;
            this._categoryService = categoryService;
            this._packageService = packageService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            var list = _packageService.GetAllPackageByPaging(term: "", count: 20, page: 0);
            return View(list);
        }
        [HttpGet]
        public virtual ActionResult CreatePackage()
        {
            ViewBag.Category = new SelectList(_categoryService.GetAllRoot(), "Id", "Name");
            ViewBag.SubCategory = new SelectList(new List<Category> { }, "Id", "Name");
            return View();
        }
        //the two below action method used for create Package
        public virtual JsonResult CalculatedUltimated(PackageAddViewModel model)
        {
            var package = new Package { DisCountPrice = model.DisCountPrice, StartDate = model.StartDate, EndDate = model.EndDate, Explain = model.Explain, Name = model.Name, OriginalPrice = model.OriginalPrice, Percent = model.Percent };
            _packageService.Create(package);
            if (_unitOfWork.SaveAllChanges() > 0)
            {
                int Id = package.Id;
                return Json(new { Id = Id }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Id = -1 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public virtual JsonResult UploadFiles(HttpPostedFileBase controlUpload, int id, string array, bool isShow, int SubCategory_Id)
        {
            if (id != 0 && id != -1)
            {
                int[] arrayResult = Array.ConvertAll(array.Split(','), x => int.Parse(x)).ToArray();
                var productList = _productService.GetAllByPackage(arrayResult);
                var package = _packageService.GetById(id);

                var Hour = productList.Sum(x => x.Time.Hours);
                var Minute = productList.Sum(x => x.Time.Minutes);

                if (Minute > 60)
                {
                    Hour += Minute / 60;
                    Minute = Minute % 60;
                }
                package.TimeEducation = string.Format("{0}:{1}", Hour, Minute).ToString(); //TimeSpan(hours: Hour, minutes: Minute, seconds: 0);

                if (controlUpload.FileName != null)
                {
                    if (controlUpload.ContentLength > 0)
                    {
                        using (var img = Image.FromStream(controlUpload.InputStream))
                        {
                            string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Package/"), controlUpload.FileName);
                            TODO.UploadImage(img, new Size(255, 162), Server.MapPath("~/Content/Images/Package/"), fileName);
                            package.Image = fileName;
                        }
                    }
                }

                package.Category = _categoryService.GetById(SubCategory_Id);
                package.IsShow = isShow;
                package.Products = productList;
                _packageService.Update(package);
                if (_unitOfWork.SaveAllChanges() > 0)
                {
                    return Json(new { result = "success" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _packageService.Delete(_packageService.GetById(id));
                    _unitOfWork.SaveAllChanges();
                    return Json(new { result = "fail" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(new { result = "fail" }, "text/html", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult PagedIndex(int? page, int Id)
        {
            System.Threading.Thread.Sleep(1500);
            var pageNumber = page - 1 ?? 0;
            var list = _productService.GetAllProductPerCategoryInPackage(IdCategory: Id, count: 20, page: pageNumber);
            if (list == null || !list.Any())
                return Content("no-more-info");
            return PartialView(viewName: MVC.admin.Package.Views._DataShowPackage, model: list);
        }
        public virtual JsonResult Calculated_discount_atFirst(int[] ProductId, int disCount)
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult DataPackage(int? page)
        {
            var pageNumber = page ?? 0;
            var list = _packageService.GetAllPackageByPaging(term: "", count: 20, page: pageNumber);
            if (list == null || !list.Any())
                return Content("no-more-info");
            return PartialView(viewName: MVC.admin.Package.Views._DataPackage, model: list);
        }
        [HttpGet]
        public virtual ActionResult DetailsDataPackage(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var package = _packageService.GetPackageByIdPackage(id);
                if (package != null)
                    return View(model: package);
                else
                    return RedirectToAction(MVC.admin.Package.ActionNames.Index);

            }
            else
                return RedirectToAction(MVC.admin.Package.ActionNames.Index);
        }
        public virtual ActionResult LoadProductPerPackage(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var list = _packageService.GetProductPerPackage(id: id);
                return PartialView(list);
            }
            else
                return Content("");
        }
        [HttpGet]
        public virtual ActionResult UpdatePackage(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var package = _packageService.GetPackageByIdPackage(id);
                if (package != null)
                {
                    var model = new PackageEditViewModel { DisCountPrice = package.DisCountPricePackage, ImageLogo = package.ImagePackage, Id = package.Id, IsShow = package.IsShowPackage, Name = package.NamePackage, OriginalPrice = package.OriginalPricePackage, Percent = package.PercentPackage, StartDate = package.StartDatePackage, Explain = package.ExplainPackage, EndDate = package.EndDatePackage, ProductsPackage = package.ProductsPackage };
                    return View(model: model);
                }
                else
                    return RedirectToAction(MVC.admin.Package.ActionNames.Index);

            }
            else
                return RedirectToAction(MVC.admin.Package.ActionNames.Index);
        }
        [HttpPost]
        public virtual ActionResult UpdatePackage(PackageEditViewModel model)
        {
            var address = Server.MapPath("~/Content/Images/Package/");
            var package = _packageService.GetById(model.Id);

            if (package.Image != null)
            {
                if (model.Image != null)
                {
                    TODO.DeleteImage(Path.Combine(Server.MapPath("~/Content/Images/Package/") + package.Image));

                    if (model.Image.ContentLength > 0)
                    {
                        using (var img = Image.FromStream(model.Image.InputStream))
                        {
                            string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Package/"), model.Image.FileName);
                            TODO.UploadImage(img, new Size(255, 162), Server.MapPath("~/Content/Images/Package/"), fileName);
                            package.Image = fileName;
                        }
                    }
                }
            }
            else
            {
                if (model.Image != null)
                    if (model.Image.ContentLength > 0)
                    {
                        using (var img = Image.FromStream(model.Image.InputStream))
                        {
                            string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Package/"), model.Image.FileName);
                            TODO.UploadImage(img, new Size(255, 162), Server.MapPath("~/Content/Images/Package/"), fileName);
                            package.Image = fileName;
                        }
                    }
            }

            package.Explain = model.Explain;
            package.IsShow = model.IsShow;
            package.Name = model.Name;
            package.DisCountPrice = model.DisCountPrice;
            package.Percent = model.Percent;
            package.StartDate = model.StartDate;
            package.EndDate = model.EndDate;


            _packageService.Update(package);

            if (_unitOfWork.SaveAllChanges() > 0)
                return RedirectToAction(MVC.admin.Package.ActionNames.DetailsDataPackage, new { Id = model.Id });
            else
            {
                TempData["updatePackage"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate), Status = AlertMode.info });
                return RedirectToAction(MVC.admin.Package.ActionNames.UpdatePackage, new { id = model.Id });
            }

        }
        public virtual ActionResult AddProductPackage(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var list = _packageService.GetPackageByIdPackage(id);
                if (list != null)
                    return View(model: list);
                else
                    return RedirectToAction(MVC.admin.Package.ActionNames.Index);

            }
            else
                return RedirectToAction(MVC.admin.Package.ActionNames.Index);
        }
        public virtual ActionResult LoadOtherProductToPackage(int? Id)
        {

            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var package = _packageService.GetById(id);
                if (package != null)
                {
                    int[] arrayProduct_Id = package.Products.Select(x => x.Id).ToArray();
                    var list = _productService.GetAllByPackage(package.Category.Id, arrayProduct_Id);
                    return PartialView(viewName: MVC.admin.Package.Views._DataShowPackage, model: list);

                }
                else
                    return RedirectToAction(MVC.admin.Package.ActionNames.Index);

            }
            else
                return RedirectToAction(MVC.admin.Package.ActionNames.Index);
        }
        public virtual PartialViewResult DeleteProductPerPackage(int id)
        {
            var package = _packageService.GetById(id);
            int[] arrayProduct_Id = package.Products.Select(x => x.Id).ToArray();
            var list = _productService.GetAllProductByPackage(arrayProduct_Id);
            return PartialView(viewName: MVC.admin.Package.Views._DataShowPackage, model: list);
        }
        public virtual PartialViewResult SubmitAddOtherProductToPackage(int Package_Id, string array)
        {
            int[] arrayResult = Array.ConvertAll(array.Split(','), x => int.Parse(x)).ToArray();
            var productList = _productService.GetAllByPackage(arrayResult);

            var package = _packageService.GetById(Package_Id);

            package.Products.AddRange(productList);

            _packageService.Update(package);
            if (_unitOfWork.SaveAllChanges() > 0)
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
        public virtual ActionResult DeleteProductPackage(int? Id)
        {

            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var list = _packageService.GetPackageByIdPackage(id);
                if (list != null)
                    return View(model: list);
                else
                    return RedirectToAction(MVC.admin.Package.ActionNames.Index);

            }
            else
                return RedirectToAction(MVC.admin.Package.ActionNames.Index);

        }
        public virtual PartialViewResult SubmitDeleteProductPerPackage(int Package_Id, string array)
        {
            int[] arrayResult = Array.ConvertAll(array.Split(','), x => int.Parse(x)).ToArray();
            var package = _packageService.GetById(Package_Id);
            var productList = package.Products.Where(x => !arrayResult.Contains(x.Id)).ToList();
            package.Products = productList;

            _packageService.Update(package);

            if (_unitOfWork.SaveAllChanges() > 0)
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
        [HttpPost]
        public virtual ActionResult DeletePackage(int id)
        {
            var package = _packageService.GetById(id);

            if (System.IO.File.Exists(Server.MapPath("~/Content/Images/Package/" + package.Image)))
                System.IO.File.Delete(Server.MapPath("~/Content/Images/Package/" + package.Image));

            _packageService.Delete(package);
            if (_unitOfWork.SaveAllChanges() > 0)
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
        public virtual JsonResult CheckNameEdit(ExistNamePackage model)
        {
            int Id = model.Id;
            var result = _packageService.GetByName(model.Name);
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
        public virtual JsonResult CheckName(string name)
        {
            if (!_packageService.IsExistByName(name))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }
    }


}



