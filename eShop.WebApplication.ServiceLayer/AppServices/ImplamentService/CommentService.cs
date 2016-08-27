using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.ImplamentService
{
    public class CommentService : EntityService<Comment>, ICommentService
    {
        private readonly IDbSet<Comment> _dbSet;
        private IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Comment>();
        }

        public List<CommentViewModel> GetAllPaging(string term, int count, int page)
        {
            var list = _dbSet.AsNoTracking().AsQueryable<Comment>()
                .Select(x => new CommentViewModel
                {
                    DateTime = x.DateTimeComment,
                    ReadStatus = x.ReadStatus,
                    Explain = x.Explain,
                    Id = x.Id,
                    Product_Id = x.Product.Id,
                    User_Id = x.ApplicationUser.Id,
                    UserName = (x.ApplicationUser.Name != null && x.ApplicationUser.Family != null) ?
                    string.Concat(x.ApplicationUser.Name + " " + x.ApplicationUser.Family) :
                    x.ApplicationUser.UserName
                })
                .OrderBy(x => x.ReadStatus==true).Skip(page * count).Take(count).ToList();
            return list;
        }


        public List<CommentViewModel> GetAllPagingByUserId(int count, int page, int userId)
        {

            var list = _dbSet.AsNoTracking().AsQueryable<Comment>().Where(x => x.User_Id.Value == userId)
                .Select(x => new CommentViewModel
                {
                    DateTime = x.DateTimeComment,
                    DisplayStatus = x.DisplayStatus,
                    Explain = x.Explain,
                    Id = x.Id,
                    Product_Id = x.Product.Id,
                    ProductName = x.Product.Name,
                    ReadStatus = x.ReadStatus,
                    User_Id = x.ApplicationUser.Id,
                    UserName = x.ApplicationUser.UserName
                }).OrderBy(x => x.DateTime)
                .Skip(page * count).Take(count);

            return list.ToList();

        }


        public List<CommentViewModel_> GetAllCommentForProduct_(int id)
        {
            return _dbSet.AsNoTracking().Where(x => x.Product_Id == id).Where(x => x.ReadStatus == true)
                .Select(x => new CommentViewModel_
            {
                Explain = x.Explain,
                DateTime = x.DateTimeComment,
                UserName = (x.ApplicationUser.Name != null && x.ApplicationUser.Family != null) ?
                string.Concat(x.ApplicationUser.Name + " " + x.ApplicationUser.Family) : "مهمان",
                Id = x.Id
            }).ToList();
        }


        public List<CommentViewModel_> GetAllCommentUserInfo_(int count, int page, int user_Id)
        {
            return _dbSet.AsNoTracking().Where(x => x.ApplicationUser.Id == user_Id)
                .Where(x => x.DisplayStatus == true)
                .Select(x => new CommentViewModel_
                {
                    DateTime = x.DateTimeComment,
                    Explain = x.Explain.ToString(),
                    Id = x.Id,
                    product_Id = x.Product.Id,
                    product_Name = x.Product.Name
                }).OrderBy(x => x.Id).Skip(count * page).Take(count).ToList();
        }


        public List<CommentViewModel> GetAllCommentPerProduct(int productId)
        {
            var list = _dbSet.AsNoTracking().Where(x => x.Product_Id == productId)
                .Select(x => new CommentViewModel
                {
                    DateTime = x.DateTimeComment,
                    DisplayStatus = x.DisplayStatus,
                    User_Id = x.ApplicationUser.Id,
                    Explain = x.Explain,
                    Id = x.Id,
                    ReadStatus = x.ReadStatus,
                    UserName = (x.ApplicationUser.Name != null && x.ApplicationUser.Family != null) ?
                    string.Concat(x.ApplicationUser.Name, " ", x.ApplicationUser.Family) :
                    x.ApplicationUser.UserName
                }).OrderByDescending(x => x.DateTime).OrderBy(x => x.ReadStatus)
                .ToList();
            return list;
        }

        public int CountUnRead
        {
            get { return _dbSet.Where(x => x.ReadStatus == false).Count(); }
        }


        public int CountCommentUser(int id)
        {
            return _dbSet.Where(x => x.ApplicationUser.Id == id).Count();
        }


        public int GetProductId(int id)
        {
            return _dbSet.Where(x => x.Id == id).Select(x => x.Product.Id).FirstOrDefault();
        }
    }
}
