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
    public partial class BlockController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBlockService _blockService;
        public BlockController(IUnitOfWork unitOfWork, IBlockService blockService)
        {
            this._unitOfWork = unitOfWork;
            this._blockService = blockService;
        }
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult DataIPBlock(string term = "", int count = 20, int page = 0)
        {
            ViewBag.term = term;
            ViewBag.page = page;
            ViewBag.count = count;

            var list = _blockService.GetAllPaging(term, count, page);
            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _blockService.Count : list.Count;

            return PartialView(viewName: MVC.admin.Block.Views._DataIPBlock, model: list);
        }
        public virtual PartialViewResult SerachIPBlock()
        {
            return PartialView(viewName: MVC.admin.Block.Views._SearchIPBlock,
                model: _blockService.Count);
        }



        public virtual JsonResult DeleteIPBlock(int Id)
        {
            _blockService.Delete(_blockService.GetById(Id));
            if (_unitOfWork.SaveAllChanges() > 0)
                return Json(new { Status = "true" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Status = "false" }, JsonRequestBehavior.AllowGet);
        }


        public virtual ActionResult GetIPForAutoComplete(string term)
        {
            return Json(_blockService.GetIPForAutoComplete(term), JsonRequestBehavior.AllowGet);
        }
    }
}