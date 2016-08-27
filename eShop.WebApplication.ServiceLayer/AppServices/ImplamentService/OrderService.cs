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
    public class OrderService : EntityService<Order>, IOrderService
    {
        private readonly IDbSet<Order> _dbSet;
        private IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Order>();
        }

        public List<OrderViewModel> GetAllOrder(int id)
        {
            var list = _dbSet.AsNoTracking().Where(x => x.Sale.Id == id).Select(x => new OrderViewModel
            {
                Id = x.Id,
                Product_Id=x.Product_Id,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();

            return list;
        }


        public List<Order> GetAllOrderBySaleId(int id)
        {
            return _dbSet.Where(x => x.Sale.Id == id).ToList();
        }
    }
}
