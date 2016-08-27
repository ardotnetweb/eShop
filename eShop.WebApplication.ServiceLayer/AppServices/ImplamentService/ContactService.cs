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
    public class ContactService : EntityService<ContactUs>, IContactService
    {
        private readonly IDbSet<ContactUs> _dbSet;
        private IUnitOfWork _unitOfWork;
        public ContactService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<ContactUs>();
        }

        public List<ContactUsShowViewModel> GetDataTable(string term, int count, int page)
        {
            var list = _dbSet.Where(x => string.Concat(x.ApplicationUser.Name, x.ApplicationUser.Family).Contains(term))
                .Select(x => new ContactUsShowViewModel
                {
                    Id = x.Id,
                    FLName = string.Concat(x.ApplicationUser.Name, x.ApplicationUser.Family),
                    Date = x.Date,
                    Explain = x.Explain,
                    StatusRead = x.StatusRead,
                    User_Id = x.User_Id,
                    StatusAnswer = x.StatusAnswer
                }).OrderBy(x => x.Date).OrderBy(x => x.StatusRead)
                .Skip(count * page).Take(count).ToList();
            return list;
        }


        public int Count
        {
            get { return _dbSet.Count(); }
        }

        public int CountUnRead
        {
            get { return _dbSet.Where(x => x.StatusRead == false).Count(); }
        }

        public int CountRead
        {
            get { return _dbSet.Where(x => x.StatusRead == true).Count(); }
        }



        public List<ContactUsShowViewModel> GetFiveLast()
        {
            return _dbSet.Select(x => new ContactUsShowViewModel
            {
                Date = x.Date,
                Explain = x.Explain.Length > 100 ? x.Explain.Substring(0, 100) : x.Explain,
                FLName = string.Concat(x.ApplicationUser.Name, " ", x.ApplicationUser.Family),
                Id = x.Id,
                StatusRead = x.StatusRead,
                User_Id = x.User_Id
            }).OrderByDescending(x => x.Id).Take(5).ToList();
        }


        public int CountMessageUser(int id)
        {
            return _dbSet.Include(x => x.ApplicationUser).Where(x => x.ApplicationUser.Id == id).Count();
        }
    }
}
