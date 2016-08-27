using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private,protect")]
    public partial class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleService _saleService;
        public OrderController(IOrderService orderService, IUnitOfWork unitOfWork,
            ISaleService saleService)
        {
            this._orderService = orderService;
            this._unitOfWork = unitOfWork;
            this._saleService = saleService;
        }
        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual PartialViewResult GetDataOrder(int id)
        {
            var list = _orderService.GetAllOrder(id);
            return PartialView(MVC.admin.Order.Views._DataOrder, list);
        }


        public virtual ActionResult DeleteOrder(int id)
        {
            var order = _orderService.GetById(id);
            var sale = _saleService.GetById(order.Sale.Id);
            sale.Price -= order.Price;
            _saleService.Update(sale);
            _orderService.Delete(order);
            if (_unitOfWork.SaveAllChanges() > 0)
                return Json(new { Result = "true" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Result = "false" }, JsonRequestBehavior.AllowGet);

        }
    }
}