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
    public class CityService : EntityService<City>, ICityService
    {
        private readonly IDbSet<City> _dbSet;
        private IUnitOfWork _unitOfWork;
        private IProvinceService _provinceService;
        public CityService(IUnitOfWork _unitOfWork, IProvinceService _provinceService)
            : base(_unitOfWork)
        {

            this._unitOfWork = _unitOfWork;
            this._provinceService = _provinceService;
            this._dbSet = _unitOfWork.Set<City>();
        }

        public List<CityShowViewModel> GetAllCityByProvinceId(int Id)
        {
            return _dbSet.AsQueryable<City>().Where(x => x.Province.Id == Id).Select(x =>
                new CityShowViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }


        public bool IsExistCityById(int Id)
        {
            return _dbSet.Any(x => x.Id == Id);
        }


        public bool IsExistVityByName(string Name)
        {
            return _dbSet.Any(x=>x.Name.Equals(Name));
        }

        public List<City> GetAllCitiesByProvinceId(int Id)
        {
            return _dbSet.AsQueryable().Where(x => x.Province.Id == Id).ToList();
        }
    }
}
