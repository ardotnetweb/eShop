using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface ICommentService : IEntityService<Comment>
    {
        int CountUnRead { get; }
        int CountCommentUser(int id);
        List<CommentViewModel> GetAllPaging(string term, int count, int page);
        List<CommentViewModel> GetAllPagingByUserId(int count, int page, int userId);
        List<CommentViewModel_> GetAllCommentForProduct_(int id);
        List<CommentViewModel_> GetAllCommentUserInfo_(int count, int page, int user_Id);
        List<CommentViewModel> GetAllCommentPerProduct(int productId);
        int GetProductId(int id);
    }
}
