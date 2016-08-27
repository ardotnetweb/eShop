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
    public class DiscountService:EntityService<Discount>,IDiscountService
    {
        private readonly IDbSet<Discount> _dbSet;
        private IUnitOfWork _unitOfWork;
        public DiscountService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            
            this._unitOfWork = _unitOfWork;
            this._dbSet=_unitOfWork.Set<Discount>();
        }
    }
}
