using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
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
    public class LabelService : EntityService<Label>, ILabelService
    {
        private readonly IDbSet<Label> _dbSet;
        private IUnitOfWork _unitOfWork;
        public LabelService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Label>();

        }

        public bool IsExistLabel(string name)
        {
            return _dbSet.Any(x => x.Name.Equals(name));
        }


        public List<Label> GetDataTable(string term, int count, int page)
        {
            var list = _dbSet.AsNoTracking().Where(x => x.Name.Contains(term))
                        .OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();
            return list;
        }

        public int Count
        {
            get { return _dbSet.Count(); }
        }


        public List<string> GetNamesByListId(params int[] Ids)
        {
            return _dbSet.Where(x => Ids.Contains(x.Id)).Select(x => x.Name).ToList();
        }


        public int [] GetIdsByName(params string[] name)
        {
            return _dbSet.Where(x => name.Contains(x.Name)).Select(x => x.Id).ToArray();
        }


        public List<Label> GetLabelsByIds(params int[] Ids)
        {
            var list=_dbSet.AsQueryable().Where(x => Ids.Contains(x.Id)).ToList();
            return list;
        }


        public List<string> GetNameForAutoComplete(string term)
        {
            return _dbSet.Where(x => x.Name.Contains(term)).Select(x => x.Name)
                .OrderBy(x => Guid.NewGuid()).Take(10).ToList();
        }
    }
}
