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
    public class ElmahService : EntityService<ElmahModel>, IElmahService
    {
        private readonly IDbSet<ElmahModel> _dbSet;
        private IUnitOfWork _unitOfWork;
        public ElmahService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<ElmahModel>();
        }


        public int Count
        {
            get { return _dbSet.Count(); }
        }


        public List<ElmahModel> GetAllPaging(string term, int count, int page)
        {
            return _dbSet.AsQueryable<ElmahModel>().Where(x => x.IP.Contains(term))
                .OrderByDescending(x => x.Date).Skip(count * page)
                .Take(count).ToList();
        }



        public bool IsExistIP(string Ip)
        {
            return _dbSet.Any(x => x.IP.Equals(Ip));
        }

        public bool IsExist(int Id)
        {
            return _dbSet.Any(x => x.Id == Id);
        }


        public List<ElmahModel> GetAllByIP(string Ip)
        {
            return _dbSet.AsQueryable<ElmahModel>().Where(x => x.IP.Equals(Ip))
                .ToList();
        }


        public List<string> GetIPForAutoComplete(string IP)
        {
            return _dbSet.Where(x => x.IP.Contains(IP)).OrderBy(x => Guid.NewGuid())
                .Select(x => x.IP).Take(10).ToList();
        }
    }
}
