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
    public class ProvinceService : EntityService<Province>, IProvinceService
    {
        private readonly IDbSet<Province> _dbSet;
        private IUnitOfWork _unitOfWork;
        public ProvinceService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Province>();
        }
        public int Count
        {
            get { return _dbSet.Count(); }
        }


        public List<ProvinceShowViewModel> GetAllInfinitProvince()
        {
            return _dbSet.AsNoTracking().AsQueryable().Select(x => new ProvinceShowViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }




        public List<ProvinceShowViewModel> GetAllProvinceByName(string term)
        {
            return _dbSet.AsNoTracking().AsQueryable().Where(x => x.Name.Contains(term))
                .Select(x => new ProvinceShowViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }


        public bool IsExistByName(string Name)
        {
            return _dbSet.Any(x => x.Name.Equals(Name));
        }

        public bool IsExistById(int Id)
        {
            return _dbSet.Any(x => x.Id == Id);
        }
    }
}
