using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.DomainClasses.AppClasses;
using WebApplication.BaseClassWebApplication;

namespace WebApplication.Controllers
{
    public partial class FavoriteController : Controller
    {
        private readonly IFavoriteUserService _favoriteUserService;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        public FavoriteController(IFavoriteUserService favoriteUserService,
                                  IProductService productService, IUnitOfWork unitOfWork)
        {
            this._favoriteUserService = favoriteUserService;
            this._productService = productService;
            this._unitOfWork = unitOfWork;
        }

        [AjaxOnly]
        [Authorize]
        public virtual ActionResult FavoriteUser()
        {
            var arrayList = _favoriteUserService.GetAllIdProductForUser(User.Identity.GetUserId<int>());
            var list = _productService.GetAllFavoriteUser(arrayList);
            return PartialView(MVC.Favorite.Views._DataFavoriteUser, list);
        }

        [AjaxOnly]
        public virtual ActionResult CheckFavoriteProduct(int productId)
        {
            var userId = User.Identity.GetUserId<int>();
            var result = _favoriteUserService.StatusInterstUserToProduct(userId, productId);
            return PartialView(MVC.Favorite.Views._StatusButtomFavoriteUser,
                new StatusButtonFavoriteViewModel { Status = result, Product_Id = productId });
        }

        [AjaxOnly]
        [Authorize]
        public virtual ActionResult AddProductToFavorite(int? productId)
        {
            try
            {
                int _productId = productId ?? 0;
                var userId = User.Identity.GetUserId<int>();
                if (_productService.IsExistById(_productId))
                {
                    if (_productId > 0 && userId > 0)
                    {
                        _favoriteUserService.Create(new Interest { User_Id = userId, Product_Id = _productId });
                        if (_unitOfWork.SaveAllChanges() > 0)
                            return PartialView(MVC.Favorite.Views._StatusButtomFavoriteUser, new StatusButtonFavoriteViewModel { Status = true, Product_Id = _productId });
                        else
                            return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.AddFavoriteError), Status = AlertMode.warning });
                    }
                    else
                        return PartialView(viewName: MVC.admin.Shared.Views._alert, model: new AlertViewModel { Alert = AlertOperation.SurveyOperation(StatusOperation.AddFavoriteError), Status = AlertMode.warning });

                }
                else
                    return Content("");
            }
            catch
            {
                return Content("");
            }
        }
    }
}