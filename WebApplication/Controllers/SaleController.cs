using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication.BaseClassWebApplication;

namespace WebApplication.Controllers
{
    public partial class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IOrderService _orderService;
        public SaleController(ISaleService saleService, 
                              IOrderService orderService)
        {
            this._saleService = saleService;
            this._orderService = orderService;
        }

        [AjaxOnly]
        public virtual ActionResult ConfirmedSale()
        {
            return PartialView(MVC.Sale.Views._ConfirmedSale);
        }

        [AjaxOnly]
        public virtual ActionResult GetDataTableNotConfirmed()
        {
            var list = _saleService.GetAllNotConfirmed(User.Identity.GetUserId<int>());
            return PartialView(MVC.Sale.Views._DataTableSaleNotConfirmed, list);
        }

        [AjaxOnly]
        public virtual ActionResult GetDataTableConfirmed(int? page)
        {
            var pageNumber = page ?? 0;
            var list = _saleService.GetAllConfirmed(count: 3, page: pageNumber, userId: User.Identity.GetUserId<int>());
            if (list == null || !list.Any())
                return Content("no-more-info");
            return PartialView(viewName: MVC.Sale.Views._DataTableSaleConfirmed, model: list);
        }

        [AjaxOnly]
        public virtual ActionResult GetDataTableNotConfirmedOrder(int sale_Id)
        {
            var list = _orderService.GetAllOrder(sale_Id);
            return PartialView(MVC.Sale.Views._DataTableOrder, list);
        }
    }
}