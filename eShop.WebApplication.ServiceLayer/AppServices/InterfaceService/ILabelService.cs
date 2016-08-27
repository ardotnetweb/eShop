using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface ILabelService : IEntityService<Label>
    {
        int Count { get; }
        bool IsExistLabel(string name);
        List<Label> GetDataTable(string term, int count, int page);
        List<string> GetNamesByListId(params int[] Ids);
        int [] GetIdsByName(params string[] name);
        List<Label> GetLabelsByIds(params int[] Ids);

        List<string> GetNameForAutoComplete(string term);
    }
}
