using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface IContactService : IEntityService<ContactUs>
    {
        List<ContactUsShowViewModel> GetDataTable(string term, int count, int page);
        int Count { get; }
        int CountUnRead { get; }
        int CountRead { get; }
        List<ContactUsShowViewModel> GetFiveLast();
        int CountMessageUser(int id);
    }
}
