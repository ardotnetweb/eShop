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
    public interface IPackageService : IEntityService<Package>
    {
        int Count { get; }
        int CountActive { get; }
        int CountDisable { get; }
        bool IsExistByName(string name);
        Package GetByName(string name);
        List<PackageViewModel> GetAllPackageByPaging(string term, int count, int page);
        PackageViewModel GetPackageByIdPackage(int id);
        List<ProductPackageViewModel> GetProductPerPackage(int id);
        //forground
        List<PackageShowViewModel> GetManyPackage_();
        void ToDoDisablePackage();
    }
}
