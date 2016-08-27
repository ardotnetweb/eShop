using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface IFavoriteUserService : IEntityService<Interest>
    {
        int[] GetAllIdProductForUser(int Id);
        bool StatusInterstUserToProduct(int userId, int productId);
        int CountFavoriteUser(int id);
    }
}
