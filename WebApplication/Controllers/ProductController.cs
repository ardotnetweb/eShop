using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.UI;
using eShop.WebApplication.DataLayer.EntityContext;
using WebApplication.BaseClassWebApplication;

namespace WebApplication.Controllers
{
    public partial class ProductController : Controller
    {
        private ICategoryService _categoryService;
        private IProductService _productService;
        private ICompanyService _companyService;
        private IUnitOfWork _unitOfWork;
        private ICommentService _commentService;
        public ProductController(ICategoryService categoryService,
                                 IProductService productService,
                                 ICompanyService companyService,
                                 IUnitOfWork unitOfWork,
                                 ICommentService commentService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
            this._companyService = companyService;
            this._unitOfWork = unitOfWork;
            this._commentService = commentService;
        }
        public virtual ActionResult Index(int? categoryId, string categoryName)
        {

            var StatusSearch = new List<SelectListItem>
            {
                new SelectListItem {Text = "پر بازدیدترین", Value = "MoreVisited"},
                new SelectListItem {Text = "تاریخ", Value = "Date", Selected = true},
                new SelectListItem {Text = "قیمت", Value = "Price"},
                new SelectListItem {Text = "جدیدترین", Value = "New"},
                new SelectListItem {Text = "پیشنهاد ویژه", Value = "Offer"}
            };
            ViewBag.StatusSearch = StatusSearch;


            var AseDes = new List<SelectListItem>
            {
                new SelectListItem {Text = "صعودی", Value = "Ascending" , Selected = true},
                new SelectListItem {Text = "نزولی", Value = "Descending"}
            };
            ViewBag.AseDes = AseDes;



            int id;
            if (int.TryParse(categoryId.ToString(), out id))
            {
                var result = _categoryService.GetById_(id);
                if (result != null)
                {
                    var model = new CategorySearchViewModel() { CategoryId = result.Id, Categories = _categoryService.GetAllSub_(result.Parent), ParentName = result.ParentName };
                    return View(model);
                }
                else
                    return RedirectToAction(MVC.Product.ActionNames.Index, MVC.Product.Name);
            }
            else
                return RedirectToAction(MVC.Product.ActionNames.Index, MVC.Product.Name);
        }

        [AjaxOnly]
        public virtual ActionResult Search(string array, string term = "", string search = "", string order = "")
        {
            int[] arrayResult = Array.ConvertAll(array.Split(','), x => int.Parse(x)).ToArray();
            StatusSearch serachResult = (StatusSearch)Enum.Parse(typeof(StatusSearch), search);
            StatusOrder orderResult = (StatusOrder)Enum.Parse(typeof(StatusOrder), order);

            return View();
        }



        public virtual ActionResult DetailSProduct(int? productId, string productName)
        {
            ViewBag.ProductName = productName;
            int id;
            if (int.TryParse(productId.ToString(), out id))
            {
                var model = _productService.GetProductById_(id);
                if (model != null)
                {
                    var modelResult = new DetailsProductShowViewModel();
                    modelResult.ProductShowViewModel = model;
                    modelResult.SendCommentViewModel = new SendCommentViewModel();
                    _productService.IncreaseVisit_(id);
                    return View(modelResult);
                }
                return Redirect("/");
            }
            return Redirect("/");
        }

        [ChildActionOnly]
        public virtual ActionResult SimilarProduct(int Id)
        {
            int id;
            if (int.TryParse(Id.ToString(), out id))
            {
                var list = _productService.GetSimilarProduct_(id);
                return PartialView(MVC.Product.Views._SimilarSlider, list);
            }
            else
                return Redirect("/");
        }

        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult GetAllProductCompanyPerCategory(int? page = 0, int? category_Id = 0, int? company_Id = 0)
        {
            int categoryId;
            int companyId;
            if (int.TryParse(category_Id.ToString(), out categoryId) &&
                int.TryParse(company_Id.ToString(), out companyId))
            {
                if (_categoryService.IsExistById(categoryId))
                {
                    if (_companyService.IsExistById(companyId))
                    {
                        var pageNumber = page ?? 0;
                        var list = _productService.GetAllProductPerCategoryByCompanyId_(20, pageNumber, categoryId, companyId);
                        if (list == null || !list.Any())
                            return Content("no-more-info");
                        return PartialView(MVC.Product.Views._BaseInformation, list);
                    }
                    else
                        return View();
                }
                else
                    return View();
            }
            else
                return View();
        }


        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AddToCart(int product_Id, string name, double price, int quantity = 1, StatusTypeOrder type = StatusTypeOrder.EWPR)
        {
            var modelDb = _productService.GetByIdExamAddToCart(product_Id);
            if (modelDb.Id == product_Id && modelDb.Price == price)
            {
                if (_productService.CheckExistProductForAddToCart_(product_Id, price))
                {
                    if (Request.Browser.Cookies)
                    {
                        try
                        {
                            if (Request.Cookies["Basket"] != null)
                            {
                                HttpCookie cookie = Request.Cookies["Basket"];
                                string valueCookie = cookie["Cart"];

                                //Deseralize && Decrypt
                                var resultDecrypt = new EnDecryption().Decryption(valueCookie);

                                var list = SerializeList.DeseraLizeList(resultDecrypt);


                                if (list.FindIndex(x => x.Product_Id == product_Id) != -1)
                                {
                                    return Json(new { result = "true", value = "این محصول قبلا به سبد خرید شما اضافه شده است", Status = "duplicate" }, JsonRequestBehavior.AllowGet);
                                }

                                list.Add(new BasketViewModel { Name = name, Price = price, Product_Id = product_Id, Quantity = quantity, StatusTypeOrder = type , PrimaryImage=modelDb.PrimaryImage });

                                var count = list.Count();

                                //Seralize && Encrypt
                                var resultSeralize = SerializeList.SeralizeList(list);
                                var resultEncrypt = new EnDecryption().Encryption(resultSeralize);
                                cookie.Values["Cart"] = resultEncrypt;
                                cookie.Expires = DateTime.Now.AddYears(1);
                                Response.Cookies.Set(cookie);
                                return Json(new { result = "true", value = "محصول مورد نظر با موفقیت به سبد خرید اضافه شد", Status = "Success" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var list = new List<BasketViewModel>() { new BasketViewModel { Name = name, Price = price, Product_Id = product_Id, Quantity = quantity, StatusTypeOrder = type , PrimaryImage=modelDb.PrimaryImage} };
                                var resultSeralize = SerializeList.SeralizeList(list);
                                var resultEncrypt = new EnDecryption().Encryption(resultSeralize);
                                HttpCookie cookie = new HttpCookie("Basket");
                                cookie.HttpOnly = true;
                                cookie.Values["Cart"] = resultEncrypt;
                                cookie.Expires = DateTime.Now.AddYears(1);
                                Response.Cookies.Add(cookie);
                                return Json(new { result = "true", value = "محصول مورد نظر با موفقیت به سبد خرید اضافه شد", Status = "Success" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch
                        {
                            return Json(new { result = "false", value = "متاسفانه در زمان اضافه کردن محصول به سبد خرید خطایی ایجاد شده است / صفحه را دوباره تازه سازی نمایید", Status = "Fail" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                        return Json(new { result = "false", value = "خواهشمندیم کوکی مرورگر خود را فعال نمایید", Status = "Fail" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { result = "false", value = "متاسفانه در زمان اضافه کردن محصول به سبد خرید خطایی ایجاد شده است / صفحه را دوباره تازه سازی نمایید", Status = "Fail" }, JsonRequestBehavior.AllowGet);
    
            }
            else
                return Json(new { result = "false", value = "متاسفانه در زمان اضافه کردن محصول به سبد خرید خطایی ایجاد شده است / صفحه را دوباره تازه سازی نمایید", Status = "Fail" }, JsonRequestBehavior.AllowGet);
        }


        [AjaxOnly]
        [HttpPost]
        public virtual ActionResult Add(int product_Id, string name, double price)
        {
            var modelDb = _productService.GetByIdExamAddToCart(product_Id);
            if (modelDb.Id == product_Id && modelDb.Price == price)
            {
                if (_productService.CheckExistProductForAddToCart_(product_Id, price))
                {
                    if (Request.Browser.Cookies)
                    {
                        try
                        {
                            if (Request.Cookies["Basket"] != null)
                            {
                                HttpCookie cookie = Request.Cookies["Basket"];
                                string valueCookie = cookie["Cart"];

                                //Deseralize && Decrypt
                                var resultDecrypt = new EnDecryption().Decryption(valueCookie);
                                var list = SerializeList.DeseraLizeList(resultDecrypt);


                                if (list.FindIndex(x => x.Product_Id == product_Id) != -1)
                                {
                                    return Json(new { result = "true", value = "این محصول قبلا به سبد خرید شما اضافه شده است", Status = "duplicate" }, JsonRequestBehavior.AllowGet);
                                }

                                list.Add(new BasketViewModel { Name = name, Price = price, Product_Id = product_Id, Quantity = 1, StatusTypeOrder = StatusTypeOrder.EWPR, PrimaryImage = modelDb.PrimaryImage });

                                var count = list.Count();

                                //Seralize && Encrypt
                                var resultSeralize = SerializeList.SeralizeList(list);
                                var resultEncrypt = new EnDecryption().Encryption(resultSeralize);
                                cookie.Values["Cart"] = resultEncrypt;
                                cookie.Expires = DateTime.Now.AddYears(1);
                                Response.Cookies.Set(cookie);
                                return Json(new { result = "true", value = "محصول مورد نظر با موفقیت به سبد خرید اضافه شد", Status = "Success" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var list = new List<BasketViewModel>() { new BasketViewModel { Name = name, Price = price, Product_Id = product_Id, Quantity = 1, StatusTypeOrder = StatusTypeOrder.EWPR, PrimaryImage = modelDb.PrimaryImage } };
                                var resultSeralize = SerializeList.SeralizeList(list);
                                var resultEncrypt = new EnDecryption().Encryption(resultSeralize);
                                HttpCookie cookie = new HttpCookie("Basket");
                                cookie.HttpOnly = true;
                                cookie.Values["Cart"] = resultEncrypt;
                                cookie.Expires = DateTime.Now.AddYears(1);
                                Response.Cookies.Add(cookie);
                                return Json(new { result = "true", value = "محصول مورد نظر با موفقیت به سبد خرید اضافه شد", Status = "Success" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch
                        {
                            return Json(new { result = "false", value = "متاسفانه در زمان اضافه کردن محصول به سبد خرید خطایی ایجاد شده است / صفحه را دوباره تازه سازی نمایید", Status = "Fail" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                        return Json(new { result = "false", value = "خواهشمندیم کوکی مرورگر خود را فعال نمایید", Status = "Fail" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { result = "false", value = "متاسفانه در زمان اضافه کردن محصول به سبد خرید خطایی ایجاد شده است / صفحه را دوباره تازه سازی نمایید", Status = "Fail" }, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(new { result = "false", value = "متاسفانه در زمان اضافه کردن محصول به سبد خرید خطایی ایجاد شده است / صفحه را دوباره تازه سازی نمایید", Status = "Fail" }, JsonRequestBehavior.AllowGet);
        }
    }

}