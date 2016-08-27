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
    public interface IOrderService : IEntityService<Order>
    {
        List<OrderViewModel> GetAllOrder(int id);
        List<Order> GetAllOrderBySaleId(int id);
    }
}
