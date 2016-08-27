using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
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
    public class SaleService : EntityService<Sale>, ISaleService
    {
        private readonly IDbSet<Sale> _dbSet;
        private IUnitOfWork _unitOfWork;
        public SaleService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Sale>();
        }

        public bool IsExist(int id)
        {
            return _dbSet.Any(x => x.Id == id);
        }


        public List<SaleViewModel> GetAllNotConfirmed(int userId)
        {
            var list = _dbSet.AsNoTracking().Where(x => x.StatusUltimate == false)
                .Where(x => x.User_Id == userId).Select(x =>
                new SaleViewModel
                {
                    Date = x.Date,
                    Id = x.Id,
                    Postage = x.Postage,
                    Price = x.Price,
                    StatusTypePay = x.StatusTypePay
                }).ToList();
            return list;
        }

        public List<SaleViewModel> GetAllConfirmed(int count, int page, int userId)
        {
            return _dbSet.AsNoTracking().Where(x => x.StatusUltimate == true).
                Where(x => x.User_Id == userId).Select(x =>
                new SaleViewModel
                {
                    Date = x.Date,
                    Id = x.Id,
                    Postage = x.Postage,
                    Price = x.Price,
                    StatusTypePay = x.StatusTypePay
                })
                .OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();
        }


        public List<ChartSaleViewModel> GetSaleSevenDayAge()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-7);

            var list = _dbSet.AsNoTracking().Where(x => x.Date >= startDate && x.Date < endDate)
                .Select(x => new ChartSaleViewModel
                {
                    Date = x.Date,
                    Price = x.Price
                }).ToList();
            return list;
        }


        public List<SaleViewModel> GetDataTable(string term, int count, int page)
        {
            return _dbSet.AsNoTracking().Where(x => string.Concat(x.ApplicationUser.Name, x.ApplicationUser.Family).Contains(term))
                .Where(x => x.StatusUltimate == true).OrderBy(x => x.Id).Select(x =>
                    new SaleViewModel
                    {
                        Date = x.Date,
                        Id = x.Id,
                        Postage = x.Postage,
                        Price = x.Price,
                        StatusSend = x.StatusSend,
                        StatusTypePay = (x.StatusTypePay == StatusTypePay.Online.ToString() ? "آنلاین" : "پرداخت به پست"),
                        FLName = string.Concat(x.ApplicationUser.Name, "  ", x.ApplicationUser.Family),
                        TrackingNumber = x.TrackingNumber,
                        User_Id = x.ApplicationUser.Id
                    }).Skip(count * page)
                .Take(count).ToList();
        }



        public int CountConfirmed
        {
            get { return _dbSet.Where(x => x.StatusUltimate == true).Count(); }
        }

        public int Count
        {
            get { return _dbSet.Count(); }
        }

        public int CountNotConfirmed
        {
            get { return _dbSet.Where(x => x.StatusUltimate == false).Count(); }
        }


        public SaleViewModel GetSaleById(int id)
        {
            return _dbSet.AsNoTracking().Where(x => x.Id == id).Select(x => new SaleViewModel
            {
                Date = x.Date,
                FLName = string.Concat(x.ApplicationUser.Name, " ", x.ApplicationUser.Family),
                Id = x.Id,
                Postage = x.Postage,
                Price = x.Price,
                StatusSend = x.StatusSend,
                StatusTypePay = (x.StatusTypePay == StatusTypePay.Online.ToString() ? "آنلاین" : "پرداخت به مامور پست"),
                TrackingNumber = x.TrackingNumber,
                User_Id = x.ApplicationUser.Id,
                StatusUltimate = x.StatusUltimate
            }).FirstOrDefault();
        }


        public List<SaleViewModel> GetAllDataTable(string term, int count, int page)
        {
            return _dbSet.AsNoTracking().Where(x => string.Concat(x.ApplicationUser.Name, x.ApplicationUser.Family).Contains(term))
                  .OrderBy(x => x.Date).OrderBy(x => x.StatusUltimate).Select(x =>
                        new SaleViewModel
                        {
                            Date = x.Date,
                            Id = x.Id,
                            Postage = x.Postage,
                            Price = x.Price,
                            StatusSend = x.StatusSend,
                            StatusTypePay = (x.StatusTypePay == StatusTypePay.Online.ToString() ? "آنلاین" : "پرداخت به پست"),
                            FLName = string.Concat(x.ApplicationUser.Name, "  ", x.ApplicationUser.Family),
                            TrackingNumber = x.TrackingNumber,
                            User_Id = x.ApplicationUser.Id,
                            StatusUltimate = x.StatusUltimate
                        }).Skip(count * page)
                    .Take(count).ToList();
        }


        public List<SaleViewModel> GetAllPagingByUserId(int count, int page, int userId)
        {
            var list = _dbSet.AsNoTracking().Where(x => x.User_Id == userId).Select(x => new SaleViewModel
            {
                Id = x.Id,
                Date = x.Date,
                Price = x.Price,
                User_Id = x.ApplicationUser.Id,
                StatusUltimate = x.StatusUltimate,
                StatusTypePay = (StatusTypePay.Online.ToString() == x.StatusTypePay ? "آنلاین" : "پرداخت به مامور پست")
            }).OrderBy(x => x.Date).Skip(count * page).Take(count).ToList();
            return list;
        }


        public int CountNotConfirmedUser(int id)
        {
            return _dbSet.Include(x=>x.ApplicationUser).Where(x => x.ApplicationUser.Id == id).Where(x => x.StatusUltimate == false).Count();
        }

        public int CountProcessUser(int id)
        {
            return _dbSet.Include(x=>x.ApplicationUser).Where(x => x.ApplicationUser.Id == id).Where(x => (x.StatusUltimate == true) && (x.StatusSend == false)).Count();
        }

        public int CountSaleUser(int id)
        {
            return _dbSet.Include(x=>x.ApplicationUser).Where(x => x.ApplicationUser.Id == id).Count();
        }

        public int CountSendUser(int id)
        {
            return _dbSet.Include(x=>x.ApplicationUser).Where(x => x.ApplicationUser.Id == id).Where(x => x.StatusSend == true).Count();
        }
    }
}
