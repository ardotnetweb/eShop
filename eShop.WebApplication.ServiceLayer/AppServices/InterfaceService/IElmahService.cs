using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface IElmahService : IEntityService<ElmahModel>
    {
        int Count { get; }
        List<ElmahModel> GetAllPaging(string term, int count, int page);
        bool IsExistIP(string Ip);
        bool IsExist(int Id);
        List<ElmahModel> GetAllByIP(string Ip);
        List<string> GetIPForAutoComplete(string IP);
    }
}
