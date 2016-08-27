using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface IImageProductService : IEntityService<ImageProduct>
    {
        List<ImageProduct> GetAllBySerachIdProduct(int Product_Id);
        List<ImageProduct> GetAllBySerachIdProduct(int Product_Id,string Name);

    }
}
