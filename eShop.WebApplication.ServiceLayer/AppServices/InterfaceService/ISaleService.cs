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
    public interface ISaleService : IEntityService<Sale>
    {
        bool IsExist(int id);
        int CountConfirmed { get; }
        int Count { get; }
        int CountNotConfirmed { get; }
        List<SaleViewModel> GetAllNotConfirmed(int userId);
        List<SaleViewModel> GetAllConfirmed(int count, int page, int userId);

        List<ChartSaleViewModel> GetSaleSevenDayAge();
        List<SaleViewModel> GetDataTable(string term, int count, int page);
        List<SaleViewModel> GetAllDataTable(string term, int count, int page);
        SaleViewModel GetSaleById(int id);
        List<SaleViewModel> GetAllPagingByUserId(int count, int page, int userId);



        int CountNotConfirmedUser(int id);
        int CountProcessUser(int id);
        int CountSaleUser(int id);
        int CountSendUser(int id);
    }
}
