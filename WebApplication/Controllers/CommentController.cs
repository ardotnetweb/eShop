using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using HtmlCleaner;
using WebApplication.BaseClassWebApplication;

namespace WebApplication.Controllers
{
    public partial class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        public CommentController(ICommentService commentService, 
                                 IUnitOfWork unitOfWork,
                                 IProductService productService)
        {
            this._commentService = commentService;
            this._unitOfWork = unitOfWork;
            this._productService = productService;
        }

        [AjaxOnly]
        public virtual ActionResult BaseCommentUserInfo()
        {
            return PartialView(MVC.Comment.Views._BaseCommentUserInfo);
        }

        [AjaxOnly]
        public virtual ActionResult GetCommentUserInfo(int? page)
        {
            try
            {
                var pageNumber = page ?? 0;
                var list = _commentService.GetAllCommentUserInfo_(count: 3, page: pageNumber, user_Id: User.Identity.GetUserId<int>());
                if (list == null || !list.Any())
                    return Content("no-more-info");
                return PartialView(viewName: MVC.Comment.Views._DataCommentUserInfo, model: list);
            }
            catch
            {
                return Content("");
            }
        }

        [AjaxOnly]
        public virtual ActionResult SendComment(DetailsProductShowViewModel model)
        {
            var userId = User.Identity.GetUserId<int>();
            var product_Id = model.ProductShowViewModel.Id;
            if (_productService.IsExistById(product_Id))
            {
                var comment = new Comment() { User_Id = userId, DateTimeComment = DateTime.Now, DisplayStatus = false, Explain = model.SendCommentViewModel.Explain.Replace("\r\n", "<br />").ToSafeHtml(), Product_Id = product_Id };
                _commentService.Create(comment);
                if (_unitOfWork.SaveAllChanges() > 0)
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.SuccessSendComment), Status = AlertMode.success });
                else
                    return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailSendComment), Status = AlertMode.warning });
            }
            else
                return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.FailSendComment), Status = AlertMode.warning });
        }

        public virtual ActionResult GetAllCommentForProduct(int id)
        {
            var list = _commentService.GetAllCommentForProduct_(id);
            return PartialView(MVC.Comment.Views._DataCommentPerProduct, list);
        }
    }
}