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
    public interface ICityService : IEntityService<City>
    {
        List<CityShowViewModel> GetAllCityByProvinceId(int Id);
        bool IsExistCityById(int Id);
        bool IsExistVityByName(string Name);

        List<City> GetAllCitiesByProvinceId(int Id);
    }
}
