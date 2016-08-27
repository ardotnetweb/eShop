using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface IBlockService : IEntityService<BlockModel>
    {
        int Count { get; }
        List<BlockModel> GetAllPaging(string term, int count, int page);
        List<string> GetIPForAutoComplete(string IP);
    }
}
