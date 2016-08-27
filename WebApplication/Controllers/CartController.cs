using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BaseClassWebApplication;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public partial class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleService _saleService;
        private readonly IApplicationUserManager _userManager;
        public CartController(IUnitOfWork unitOfWork, 
                              ISaleService saleService,
                              IApplicationUserManager userManager)
        {
            this._saleService = saleService;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        public virtual ActionResult Index()
        {
            var list = new List<BasketViewModel>();
            if (Request.Browser.Cookies)
            {
                if (Request.Cookies["Basket"] != null)
                {
                    HttpCookie cookie = Request.Cookies["Basket"];
                    string valueCookie = cookie["Cart"];
                    var resultDecrypt = new EnDecryption().Decryption(valueCookie);
                    list = SerializeList.DeseraLizeList(resultDecrypt);
                    return View(list);
                }
                else
                    return View(list);
            }
            else
                return RedirectToAction("/");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Index(List<BasketViewModel> model)
        {
            if (Request.Cookies["Basket"] != null)
            {
                var newList = new List<BasketViewModel>();
                HttpCookie cookie = Request.Cookies["Basket"];
                string valueCookie = cookie["Cart"];

                //Deseralize && Decrypt
                var resultDecrypt = new EnDecryption().Decryption(valueCookie);
                var previouslist = SerializeList.DeseraLizeList(resultDecrypt);

                foreach (var item in model)
                {
                    if (previouslist.FindIndex(x => x.Product_Id == item.Product_Id) == -1) continue;
                    var successModel = previouslist.Find(x => x.Product_Id == item.Product_Id);
                    successModel.Quantity = item.Quantity;
                    newList.Add(successModel);
                }

                var resultSeralize = SerializeList.SeralizeList(newList);
                var resultEncryption = new EnDecryption().Encryption(resultSeralize);

                cookie.Values["Cart"] = resultEncryption;
                Response.Cookies.Set(cookie);

                return RedirectToAction(MVC.Cart.ActionNames.Information);
            }
            else
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        [AjaxOnly]
        public virtual ActionResult RemoveItem(int product_Id)
        {
            var newList = new List<BasketViewModel>();
            HttpCookie cookie = Request.Cookies["Basket"];
            string valueCookie = cookie["Cart"];

            //Deseralize && Decrypt
            var resultDecrypt = new EnDecryption().Decryption(valueCookie);
            var list = SerializeList.DeseraLizeList(resultDecrypt);

            if (list.FindIndex(x => x.Product_Id == product_Id) != -1)
            {
                list.Remove(list.Find(x => x.Product_Id == product_Id));
                var resultSerialize = SerializeList.SeralizeList(list);
                var resultEncryption = new EnDecryption().Encryption(resultSerialize);
                cookie.Values["Cart"] = resultEncryption;
                Response.Cookies.Add(cookie);
                return Json(new { result = "true" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { result = "false" }, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        public async virtual Task<ActionResult> Information()
        {
            if (Request.Cookies["Basket"] != null)
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
                return PartialView(model);
            }
            else
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        public async virtual Task<ActionResult> Confirmation()
        {
            var list = new List<BasketViewModel>();
            if (Request.Browser.Cookies)
            {
                if (Request.Cookies["Basket"] != null)
                {
                    HttpCookie cookie = Request.Cookies["Basket"];
                    string valueCookie = cookie["Cart"];
                    var resultDecrypt = new EnDecryption().Decryption(valueCookie);
                    list = SerializeList.DeseraLizeList(resultDecrypt);
                    var _user = await _userManager.FindUserByIdForShow_(User.Identity.GetUserId<int>());
                    ViewBag.user = _user;
                    return View(list);
                }
                else
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            else
                return RedirectToAction("/");
        }

        [HttpGet]
        public virtual ActionResult Payment()
        {
            if (Request.Cookies["Basket"] != null)
                return View();
            else
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Payment(StatusTypePay selectPayment = StatusTypePay.Online)
        {
            var list = new List<BasketViewModel>();
            if (Request.Browser.Cookies)
            {
                if (Request.Cookies["Basket"] != null)
                {
                    HttpCookie cookie = Request.Cookies["Basket"];
                    string valueCookie = cookie["Cart"];
                    var resultDecrypt = new EnDecryption().Decryption(valueCookie);
                    list = SerializeList.DeseraLizeList(resultDecrypt);
                    var sale = new Sale
                    {
                        Date = DateTime.Now,
                        Postage = 8000,
                        Price = list.Sum(x => x.Price),
                        StatusTypePay = selectPayment.ToString(),
                        StatusUltimate = false,
                        StatusSend = false,
                        User_Id = User.Identity.GetUserId<int>(),
                    };

                    var orderList = new List<Order>();
                    list.ForEach(x =>
                    {
                        orderList.Add(new Order
                        {
                            Name = x.Name,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            StatusTypeOrder = StatusTypeOrder.EWPR.ToString(),
                            Product_Id = x.Product_Id
                        });
                    });
                    sale.Orders = orderList;

                    _saleService.Create(sale);
                    if (_unitOfWork.SaveAllChanges() > 0)
                    {
                        HttpCookie cookieCode = new HttpCookie("TrackingNumber");
                        cookieCode.Values["Code"] = new EnDecryption().Encryption(((sale.Id * 347) + 2323).ToString());
                        cookieCode.Expires = DateTime.Now.AddMinutes(10);
                        Response.Cookies.Add(cookieCode);

                        if (selectPayment == StatusTypePay.Online)
                            return Json(new { Status = "true", Result = "ثبت سفارش با موفقیت صورت گرفت", Address = "Ultimate/" }, JsonRequestBehavior.AllowGet);
                        else
                            return Json(new { Status = "true", Result = "ثبت سفارش با موفقیت صورت گرفت", Address = "Ultimate/" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { Status = "false", Result = "متاسفانه در ثبت سفارش خطایی رخ داده شده است مجدد صفحه را بروز رسانی کنید" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            else
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        public virtual ActionResult Ultimate()
        {
            int id;
            if (Request.Cookies["TrackingNumber"] != null)
            {
                string code = new EnDecryption().Decryption(Request.Cookies["TrackingNumber"].Values["Code"]);
                if (int.TryParse(code, out id))
                {
                    var result = _saleService.IsExist((id - 2323) / 347);
                    if (result)
                    {

                        ViewBag.Code = id;
                        HttpCookie cookiCode = Request.Cookies["TrackingNumber"];
                        HttpCookie cookiBasket = Request.Cookies["Basket"];


                        cookiBasket.Expires = DateTime.Now.AddDays(-1);
                        cookiCode.Expires = DateTime.Now.AddDays(-1);


                        Response.Cookies.Remove("TrackingNumber");
                        Response.Cookies.Remove("Basket");
                        Response.Cookies["TrackingNumber"].Expires = DateTime.Now.AddSeconds(-1);
                        Response.Cookies["Basket"].Expires = DateTime.Now.AddSeconds(-1);
                        return View();
                    }
                    else
                        return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
                }
                else
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            else
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }
    }
}