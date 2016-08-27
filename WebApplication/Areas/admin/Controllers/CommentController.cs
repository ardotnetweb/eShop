using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity;

namespace WebApplication.Areas.admin.Controllers
{
    [Authorize(Roles = "private")]
    public partial class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IProductService _productService;
        private IUnitOfWork _unitOfWork;
        public CommentController(ICommentService commentService, IUnitOfWork unitOfWork,
            IProductService productService)
        {
            this._commentService = commentService;
            this._unitOfWork = unitOfWork;
            this._productService = productService;
        }
        public virtual ActionResult Index()
        {
            var list = _commentService.GetAllPaging(term: string.Empty, count: 20, page: 0);
            return View(list);
        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult PagedIndex(int? page)
        {
            var pageNumber = page ?? 0;
            var list = _commentService.GetAllPaging(term: string.Empty, count: 5, page: pageNumber);
            if (list == null || !list.Any())
                return Content("no-more-info");
            return PartialView(viewName: MVC.admin.Comment.Views._DataComment, model: list);
        }

        [HttpGet]
        public virtual ActionResult Post(int? id)
        {
            if (id == null)
                return Redirect("/");
            return Content("Post " + id.Value);
        }

        [HttpGet]
        public virtual ActionResult ResponseComment(int Id)
        {
            return View(model: _commentService.GetById(Id: Id));
        }


        [HttpPost]
        public virtual PartialViewResult ResponseComment(Comment comment)
        {
            return PartialView();
        }


        [HttpGet]
        public virtual ActionResult UpdateComment(int Id)
        {
            return View(model: _commentService.GetById(Id: Id));
        }




        [HttpPost]
        public virtual PartialViewResult UpdateComment(Comment comment)
        {
            return PartialView();
        }


        public virtual ActionResult GetAllCommentPerProduct(int Id)
        {
            var list = _commentService.GetAllCommentPerProduct(Id);
            return PartialView(MVC.admin.Comment.Views._DataCommentPerProduct, list);
        }




        public virtual ActionResult ChangeStatusRead(int Id)
        {
            var productId = _commentService.GetProductId(Id);
            var product = _productService.GetById(productId);
            product.ModifiedDate = DateTime.Now;
            _productService.Update(product);


            var comment = _commentService.GetById(Id);
            comment.ReadStatus = true;
            _commentService.Update(comment);
            if (_unitOfWork.SaveAllChanges() > 0)
            {
                return Json(new { Result = "true" }, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(new { Result = "false" }, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult DeleteComment(int Id)
        {
            var productId = _commentService.GetProductId(Id);
            var product = _productService.GetById(productId);
            product.ModifiedDate = DateTime.Now;
            _productService.Update(product);


            var comment = _commentService.GetById(Id);
            _commentService.Delete(comment);
            if (_unitOfWork.SaveAllChanges() > 0)
                return Json(new { Result = "true" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Result = "false" }, JsonRequestBehavior.AllowGet);
        }



        public virtual ActionResult SendComment(int Id, string ExplainComment)
        {
            var productId = _commentService.GetProductId(Id);
            var product = _productService.GetById(productId);
            product.ModifiedDate = DateTime.Now;
            _productService.Update(product);


            var comment = new Comment() { User_Id = User.Identity.GetUserId<int>(), DateTimeComment = DateTime.Now, ReadStatus = true, Explain = ExplainComment.Replace("\r\n", "<br />"), Product_Id = Id };
            _commentService.Create(comment);
            if (_unitOfWork.SaveAllChanges() > 0)
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessSendComment), Status = AlertMode.success });
            else
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailSendComment), Status = AlertMode.warning });
        }
    }

}