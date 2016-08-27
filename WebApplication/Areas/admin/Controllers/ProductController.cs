using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using HtmlCleaner;
using System.Web.Mvc;
using WebApplication.BaseClassWebApplication;
using WebApplication.Lucene;
//using WebApplication.Lucene;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    public partial class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private ICompanyService _companyService;
        private IImageProductService _imageProductService;
        private ILabelService _labelService;
        private IFavoriteUserService _favoriteService;
        public ProductController(IUnitOfWork unitOfWork,
                                 IProductService productService,
                                 ICategoryService categoryService,
                                 ICompanyService companyService,
                                 IImageProductService imageProductService,
                                 ILabelService labelService,
                                 IFavoriteUserService favoriteService)
        {
            this._unitOfWork = unitOfWork;
            this._productService = productService;
            this._categoryService = categoryService;
            this._companyService = companyService;
            this._imageProductService = imageProductService;
            this._labelService = labelService;
            this._favoriteService = favoriteService;
        }

        public virtual ViewResult Index()
        {
            return View();
        }
        public virtual PartialViewResult DataProduct(string term = "", int count = 20, int page = 0)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;
            var list = _productService.GetDataTable(term, count, page);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _productService.Count : list.Count;
            return PartialView(viewName: MVC.admin.Product.Views._DataProduct, model: list);
        }
        public virtual PartialViewResult SearchProduct()
        {
            return PartialView(viewName: MVC.admin.Product.Views._SearchProduct, model: _productService.Count.ToString());
        }
        [HttpGet]
        public virtual ActionResult DetailsProduct(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var product = _productService.GetShowProductById(id: id);
                if (product != null)
                {
                    var resultLabel = product.Labels.Select(x => x.Name).ToList();
                    product.LabelsName = string.Join(",", resultLabel);
                    return View(model: product);
                }
                else
                    return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index,
                            controllerName: MVC.admin.Product.Name);
            }
            else
                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index,
                    controllerName: MVC.admin.Product.Name);
        }
        public virtual ActionResult CreateProduct()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAllSubForDropdown(), "Category_Id", "Name");
            ViewBag.Companies = new SelectList(_companyService.GetAllForDropdown(), "Company_Id", "Name");
            ViewBag.Labels = new MultiSelectList(_labelService.GetAll(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public virtual ActionResult CreateProduct(ProductViewModel model, IEnumerable<HttpPostedFileBase> ImageProductAdditinal)
        {
            var product = new Product() { Explain = model.Explain, Category = _categoryService.GetById(model.Category_Id), Company = _companyService.GetById(model.Company_Id), Name = model.Name, Labels = _labelService.GetLabelsByIds(model.Labels), Time = model.Time, Date = model.Date, VisitNumber = 0, Price = model.Price, Recomend = 1, ModifiedDate = DateTime.Now };

            //save primary image with 3 size
            if (model.Image != null)
            {
                if (model.Image.ContentLength > 0)
                {
                    using (var img = Image.FromStream(model.Image.InputStream))
                    {
                        string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Product/MainSize/"), model.Image.FileName);

                        TODO.UploadImage(img, new Size(180, 180), Server.MapPath("~/Content/Images/Product/MainSize/"), fileName);
                        TODO.UploadImage(img, new Size(70, 70), Server.MapPath("~/Content/Images/Product/ThumbLine_New/"), fileName);
                        TODO.UploadImage(img, new Size(60, 75), Server.MapPath("~/Content/Images/Product/ThumbLine_Offer"), fileName);
                        product.PrimryImage = fileName;
                    }
                }
            }

            //save other image for this prduct
            var listImage = new List<ImageProduct>();
            foreach (var item in ImageProductAdditinal)
            {
                if (item != null && item.ContentLength > 0)
                {
                    string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Product/GallerySize/"), model.Image.FileName);

                    TODO.UploadImage(Image.FromStream(item.InputStream), new Size(450, 450), Server.MapPath("~/Content/Images/Product/GallerySize/"), fileName);
                    listImage.Add(new ImageProduct { Image = fileName, Product = product });
                }
            }


            product.ImageProducts = listImage;
            _productService.Create(product);

            if (_unitOfWork.SaveAllChanges() > 0)
            {
                //For Use Notification With SignalR
                //Response Redirect To Main Page In Site 
                //return RedirectToAction(MVC.admin.Product.ActionNames.DetailsProduct, new { Id = product.Id });
                if (Request.Cookies["Cart"] != null)
                {
                    HttpCookie cookie = Request.Cookies["Cart"];
                    cookie.Value = "true";
                    cookie.Expires = DateTime.Now.AddMinutes(2);
                    System.Web.HttpContext.Current.Response.Cookies.Set(cookie);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("Alert");
                    cookie.Value = "true";
                    cookie.Expires = DateTime.Now.AddMinutes(2);
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }

                LuceneProductSearch.AddUpdateLuceneIndex(new LuceneProductModel
                {
                    Product_Id = product.Id,
                    Product_Name = product.Name,
                    Product_Explain = product.Explain.Length > 400 ? 
                        product.Explain.ToSafeHtml().RemoveHtmlTags().Substring(0, 400) :
                        product.Explain.ToSafeHtml().RemoveHtmlTags()
                });

                return RedirectToRoute("DetailsProduct", new { productId = product.Id, productName = UrlExtensions.ResolveTitleForUrl(product.Name) });
            }
            else
            {
                TempData["createProduct"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailInsert), Status = AlertMode.warning });
                return RedirectToAction(MVC.admin.Product.ActionNames.CreateProduct);
            }
        }

        [HttpGet]
        public virtual ActionResult UpdateProduct(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var list = _productService.GetEditProductById(id: id);
                if (list != null)
                {

                    List<int> LabelsId = list.Labels.Select(x => x.Id).ToList();
                    ViewBag.Categories = new SelectList(_categoryService.GetAllSubForDropdown(), "Category_Id", "Name");
                    ViewBag.Companies = new SelectList(_companyService.GetAllForDropdown(), "Company_Id", "Name");
                    ViewBag.Labels = new MultiSelectList(_labelService.GetAll(), "Id", "Name", LabelsId);
                    return View(model: list);
                }
                else
                    return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);

            }
            else
                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);
        }
        [HttpPost]
        public virtual ActionResult UpdateProduct(ProductEditViewModel model, string _PrimryImage)
        {
            var product = _productService.GetById(model.Id);
            if (model.Image != null)
            {
                if (model.Image.ContentLength > 0)
                {
                    //delete with 3 address
                    TODO.DeleteImage(Path.Combine(Server.MapPath("~/Content/Images/Product/MainSize/") + _PrimryImage));
                    TODO.DeleteImage(Path.Combine(Server.MapPath("~/Content/Images/Product/ThumbLine_New/") + _PrimryImage));
                    TODO.DeleteImage(Path.Combine(Server.MapPath("~/Content/Images/Product/ThumbLine_Offer") + _PrimryImage));

                    //add with 3 address
                    using (var img = Image.FromStream(model.Image.InputStream))
                    {
                        string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Product/MainSize/"), model.Image.FileName);

                        TODO.UploadImage(img, new Size(180, 180), Server.MapPath("~/Content/Images/Product/MainSize/"), fileName);
                        TODO.UploadImage(img, new Size(70, 70), Server.MapPath("~/Content/Images/Product/ThumbLine_New/"), fileName);
                        TODO.UploadImage(img, new Size(60, 75), Server.MapPath("~/Content/Images/Product/ThumbLine_Offer"), fileName);
                        product.PrimryImage = fileName;
                    }
                }
            }

            product.Explain = model.Explain; product.Name = model.Name; product.Time = model.Time; product.Date = model.Date; product.Price = model.Price; product.Company = _companyService.GetById(model.Company_Id); product.Category = _categoryService.GetById(model.Category_Id);

            product.Labels.Clear();
            product.Labels = null;
            if (model.LabelsId != null)
            {
                var labels = _labelService.GetLabelsByIds(model.LabelsId);
                product.Labels = labels;
            }
            else
                product.Labels = null;

            product.ModifiedDate = DateTime.Now;

            _productService.Update(product);
            if (_unitOfWork.SaveAllChanges() > 0)
            {
                //Remove Id From Document
                LuceneProductSearch.ClearLuceneIndexRecord(model.Id);

                //Add New Document
                LuceneProductSearch.AddUpdateLuceneIndex(new LuceneProductModel
                {
                    Product_Id = model.Id,
                    Product_Explain = model.Explain.ToSafeHtml().RemoveHtmlTags(),
                    Product_Name = model.Name
                });

                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.DetailsProduct, routeValues: new { Id = model.Id });
            }
            else
            {
                TempData["createProduct"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate), Status = AlertMode.warning });
                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.UpdateProduct, routeValues: new { Id = model.Id });
            }
        }

        [HttpGet]
        public virtual ActionResult ShowImageProduct(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var imageProduct = _productService.GetImageProductById(id: id);
                if (imageProduct != null)
                    return View(model: imageProduct);
                else
                    return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);

            }
            else
                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);
        }

        [HttpGet]
        public virtual ActionResult CreateImageProduct(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                if (_productService.IsExistById(id))
                {
                    var list = _productService.GetImageProductById(id: id);
                    return View(model: list);
                }
                else
                    return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);
            }
            else
                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);

        }
        [HttpPost]
        public virtual ActionResult CreateImageProduct(IEnumerable<HttpPostedFileBase> ImageProductAdditinal, int Id)
        {
            var model = _productService.GetById(Id: Id);
            var imageProductList = new List<ImageProduct>();

            foreach (var item in ImageProductAdditinal)
            {
                if (item != null)
                    if (item.ContentLength > 0)
                    {
                        using (var img = Image.FromStream(item.InputStream))
                        {
                            string fileName = TODO.CheckExistFile(Server.MapPath("~/Content/Images/Product/GallerySize/"), item.FileName);

                            TODO.UploadImage(img, new Size(600, 600), Server.MapPath("~/Content/Images/Product/GallerySize/"), fileName);
                            imageProductList.Add(new ImageProduct { Image = fileName, Product = model });
                        }
                    }
            }
            _unitOfWork.AddRange<ImageProduct>(imageProductList);

            if (_unitOfWork.SaveAllChanges() > 0)
                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.ShowImageProduct, routeValues: new { Id = Id });
            else
            {
                TempData["createImageProduct"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Dependencies), Status = AlertMode.info });
                return RedirectToAction(MVC.admin.Product.ActionNames.CreateImageProduct, new { Id = Id });
            }
        }
        [HttpGet]
        public virtual ActionResult DeleteImageProduct(int? Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {

                if (_productService.IsExistById(id))
                {
                    var list = _productService.GetImageProductById(id: id);
                    if (list != null)
                        return View(model: list);
                    else
                        return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);
                }
                else
                    return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);

            }
            else
                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.Index);
        }

        [HttpPost]
        public virtual ActionResult DeleteImageProduct(List<string> checkboxImage, int Id)
        {

            var list = new List<ImageProduct>();
            var address = Server.MapPath("~/Content/Images/Product/GallerySize/");

            foreach (var item in checkboxImage)
            {
                list.Add(new ImageProduct { Id = int.Parse(item.Split('/')[0]) });
                TODO.DeleteImage(Path.Combine(address, item.Split('/')[1]));
            }

            _unitOfWork.RemoveRange<ImageProduct>(list);
            if (_unitOfWork.SaveAllChanges() > 0)
            {
                //Remove Id From Document
                LuceneProductSearch.ClearLuceneIndexRecord(Id);
                return RedirectToAction(actionName: MVC.admin.Product.ActionNames.DetailsProduct, routeValues: new { Id = Id });
            }
            else
            {
                ViewData["deleteImageProduct"] = Helperalert.alert(new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.Dependencies), Status = AlertMode.info });
                return RedirectToAction(MVC.admin.Product.ActionNames.DeleteImageProduct, new { Id = Id });
            }
        }


        [HttpPost]
        public virtual PartialViewResult DeleteProduct(int Id)
        {
            var imageProduct = _imageProductService.GetAllBySerachIdProduct(Product_Id: Id);
            if (imageProduct != null)
            {
                _unitOfWork.RemoveRange(imageProduct);
            }
            var product = _productService.GetById(Id: Id);
            _productService.Delete(product);
            if (_unitOfWork.SaveAllChanges() > 0)
            {
                //Delete Dependence Image
                foreach (var item in imageProduct)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/Images/Product/" + item.Image)))
                        System.IO.File.Delete(Server.MapPath("~/Content/Images/Product/" + item.Image));
                }
                //Delete Primary Image
                if (System.IO.File.Exists(Server.MapPath("~/Content/Images/Product/" + product.PrimryImage)))
                    System.IO.File.Delete(Server.MapPath("~/Content/Images/Product/" + product.PrimryImage));

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



        public virtual ActionResult OfferProduct()
        {
            ViewBag.Category = new SelectList(_categoryService.GetAllRoot(), "Id", "Name");
            ViewBag.SubCategory = new SelectList(new List<Category> { }, "Id", "Name");
            return View();
        }

        public virtual PartialViewResult DataTableOffer(int technology, string term = "", int count = 20, int page = 0)
        {
            //technology==id technology
            //the program link to Service for get information 
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;
            ViewBag.technology = technology;
            var list = _productService.GetDataTableForOffer(technology, term, count, page);

            ViewBag.TotalRecords = _productService.CountProductPerCategory(technology);
            return PartialView(viewName: MVC.admin.Product.Views._DataProductOffer, model: list);

        }



        public virtual ActionResult SaverOfferProduct(string Ids)
        {
            var array = Array.ConvertAll(Ids.Split(','), x => int.Parse(x)).ToArray();
            var list = _productService.GetAllProductByIdForOffer(array);
            foreach (var item in list)
            {
                item.Recomend = 2;
                _productService.Update(item);
            }
            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessUpdate), Status = AlertMode.success });
            else
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate), Status = AlertMode.warning });
        }

        public virtual ActionResult ChageObsverOfferProduct()
        {
            ViewBag.Category = new SelectList(_categoryService.GetAllRoot(), "Id", "Name");
            ViewBag.SubCategory = new SelectList(new List<Category> { }, "Id", "Name");
            return View();
        }

        public virtual PartialViewResult DataTableChangeObcerProduct(int id)
        {
            var list = _productService.GetDataTableInOffer(id);
            return PartialView(MVC.admin.Product.Views._DataProductChangeObsverProduct, list);
        }

        public virtual PartialViewResult DeleteOfferProduct(string Ids)
        {
            var array = Array.ConvertAll(Ids.Split(','), x => int.Parse(x)).ToArray();
            var list = _productService.GetAllProductByIdForOffer(array);
            foreach (var item in list)
            {
                item.Recomend = 1;
                _productService.Update(item);
            }
            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccsessUpdate), Status = AlertMode.success });
            else
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailUpdate), Status = AlertMode.warning });
        }




        public virtual PartialViewResult DataProductSearch(int Id, string term = "", int page = 0, int count = 20)
        {
            // System.Threading.Thread.Sleep(5000);
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;
            ViewBag.Id = Id;
            var list = _productService.GetDataTablePerCategory(Id, term, count, page);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _productService.Count : list.Count;

            return PartialView(viewName: MVC.admin.Category.Views._DataCategorySearch, model: list);
        }
        [HttpGet]
        public virtual PartialViewResult NavbarCategoryCompanySearchProduct()
        {
            IEnumerable<SelectListItem> category = new SelectList(_categoryService.GetAllSubForDropdown(), "Category_Id", "Name");
            ViewBag.Categories = category;
            IEnumerable<SelectListItem> company = new SelectList(_companyService.GetAllForDropdown(), "Company_Id", "Name");
            ViewBag.Companies = company;

            return PartialView(viewName: MVC.admin.Category.Views._NavbarCategory_CompanySearchProduct);
        }


        // Used for section User
        [HttpGet]
        public virtual PartialViewResult FavoriteListUser(int Id)
        {
            var arrayProduct = _favoriteService.GetAllIdProductForUser(Id);
            if (arrayProduct.Length > 0)
            {
                var list = _productService.GetDataTableInterestUser(arrayProduct);
                return PartialView(viewName: MVC.admin.Manage.Views._FavoriteListUser,
                    model: list);
            }
            else
            {
                return PartialView(MVC.admin.Shared.Views._alert, new AlertViewModel
                {
                    Alert = AlertOperation.SurveyOperation(StatusOperation.NoItem),
                    Status = AlertMode.White
                });
            }
        }

        // Used For section combination
        public virtual PartialViewResult DataProductFirstLoad(string term = "", int page = 0, int count = 20)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;


            var list = _productService.GetDataTable(term, count, page);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _productService.Count : list.Count;

            return PartialView(viewName: MVC.admin.Product.Views._DataProductFirstLoad, model: list);
        }
        public virtual PartialViewResult DataProductFirstLoadSearch(int? category_Id, int? company_Id, string term = "", int page = 0, int count = 20)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;


            var list = _productService.GetDataTablePerCategoryForSearch(category_Id, company_Id, term, page, count);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _categoryService.Count : list.Count;

            return PartialView(viewName: MVC.admin.Product.Views._DataProductFirstLoad, model: list);
        }


        public virtual ActionResult GetProductNameAutocomplete(string term)
        {
            var list = _productService.GetNameForAutoComplete(term);
            return Json(list, JsonRequestBehavior.AllowGet);
        }




    }
}