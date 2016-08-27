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
    public class ImageProductService : EntityService<ImageProduct>, IImageProductService
    {
        private readonly IDbSet<ImageProduct> _dbSet;
        private IUnitOfWork _unitOfWork;
        public ImageProductService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<ImageProduct>();
        }

        public List<ImageProduct> GetAllBySerachIdProduct(int Product_Id)
        {
            return _dbSet.Where(image => image.Product.Id == Product_Id).ToList();
        }


        public List<ImageProduct> GetAllBySerachIdProduct(int Product_Id, string Name)
        {
            return _dbSet.Where(image => image.Product.Id == Product_Id && image.Product.Name.Equals(Name)).ToList();
        }
    }
}
