using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.MainViewModel;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface ICompanyService : IEntityService<Company>
    {
        int Count { get; }
        bool IsExistByName(string name);
        bool IsExistByAddress(string address);
        bool IsExistById(int id);
        bool CheckCollectionId_(params int[] arrayCategory);
        ExistNameCompany GetByName(string name);
        ExistAddressCompany GetByAddress(string address);
        List<Company> GetDataTable(string term, int count, int page);
        IEnumerable<CompanyDropListViewModel> GetAllForDropdown();


        //نمایش نام کمپانی ها و تعداد محصولات و تعداد ساعات آموزش برای صفحه لیست
        List<CompanyShowViewModel> GetAllCompanyForShow_();
        //نمایش نام کمپانی ها و تعداد محصولات و تعداد ساعات آموزش برای صفحه اصلی
        List<CompanyShowViewModel> GetAllCompanyForShowRandom_();
        CompanyViewModel GetCompanyById_(int id);

        CompanyInformationToCart GetInformationForAddToCart_(int id);

        List<string> GetNameForAutoComplete(string term);
    }
}
