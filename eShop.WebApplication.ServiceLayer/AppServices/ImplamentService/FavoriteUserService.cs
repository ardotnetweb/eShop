using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
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
    public class FavoriteUserService : EntityService<Interest>, IFavoriteUserService
    {
        private readonly IDbSet<Interest> _dbSet;
        private IUnitOfWork _unitOfWork;
        public FavoriteUserService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Interest>();

        }

        public int[] GetAllIdProductForUser(int Id)
        {
            return _dbSet.AsQueryable().Where(x => x.ApplicationUser.Id == Id).
                Select(x => x.Product_Id.Value).ToArray();
        }


        public bool StatusInterstUserToProduct(int userId, int productId)
        {
            return _dbSet.Where(x => x.Product_Id == productId && x.User_Id == userId).Any();
        }


        public int CountFavoriteUser(int id)
        {
            return _dbSet.Include(x => x.ApplicationUser).Where(x => x.ApplicationUser.Id == id).Count();
        }
    }
}
