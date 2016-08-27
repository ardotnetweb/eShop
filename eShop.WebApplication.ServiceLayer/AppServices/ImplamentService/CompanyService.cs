using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.MainViewModel;
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
    public class CompanyService : EntityService<Company>, ICompanyService
    {
        private readonly IDbSet<Company> _dbSet;
        private IUnitOfWork _unitOfWork;
        public CompanyService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Company>();

        }
        public int Count
        {
            get { return _dbSet.Count(); }
        }
        public bool IsExistByName(string name)
        {
            return _dbSet.Any(company => company.Name.Equals(name));
        }
        public bool IsExistByAddress(string address)
        {
            return _dbSet.Any(company => company.Address.Equals(address));
        }
        public bool IsExistById(int id)
        {
            return _dbSet.Any(x => x.Id == id);
        }
        public ExistNameCompany GetByName(string name)
        {
            return _dbSet.Where(x => x.Name.Equals(name)).Select(x => new ExistNameCompany { Id = x.Id, Name = x.Name }).FirstOrDefault();
        }
        public ExistAddressCompany GetByAddress(string address)
        {
            return _dbSet.Where(x => x.Address.Equals(address)).Select(x => new ExistAddressCompany { Id = x.Id, Address = x.Address }).FirstOrDefault();
        }
        public List<Company> GetDataTable(string term, int count, int page)
        {
            var list = _dbSet.AsNoTracking().Where(x => x.Name.Contains(term))
                        .OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();
            return list;
        }
        public IEnumerable<CompanyDropListViewModel> GetAllForDropdown()
        {
            return _dbSet.AsNoTracking().Select(company => new CompanyDropListViewModel
            {
                Company_Id = company.Id,
                Name = company.Name
            }).ToList();
        }


        public List<CompanyShowViewModel> GetAllCompanyForShow_()
        {
            var list = _dbSet.Include(x => x.Products).ToList().Select(x => new CompanyShowViewModel
            {
                Id = x.Id,
                Name = x.Name,
                countProduct = x.Products.Count(),
                countHour = new TimeSpan(x.Products.Sum(r => r.Time.Ticks)),
                PrimaryImage = x.ImageLogo
            }).ToList();
            return list;
        }


        public bool CheckCollectionId_(params int[] arrayCategory)
        {
            bool result = true;
            if (arrayCategory!=null && arrayCategory.Length > 0)
            {
                foreach (var item in arrayCategory)
                {
                    result = _dbSet.Any(x => x.Id == item);
                    if (!result) { result = false; break; }
                }
                return result;
            }
            else
                return false;
        }


        public CompanyInformationToCart GetInformationForAddToCart_(int id)
        {
            return _dbSet.Where(x => x.Id == id).Select(x => new CompanyInformationToCart
            {
                Price = x.Products.Sum(p => p.Price),
                Quantity = x.Products.Count()
            }).FirstOrDefault();
        }


        public CompanyViewModel GetCompanyById_(int id)
        {
            return _dbSet.AsNoTracking().Where(x => x.Id == id).Select(x => new CompanyViewModel
            {
                IdCompany = x.Id,
                ImageCompany = x.ImageLogo,
                NameCompany = x.Name,
                TitleCompany = x.Title,
                ExplainCompany = x.Explain,
                AddressCompany = x.Address
            }).FirstOrDefault();
        }


        public List<CompanyShowViewModel> GetAllCompanyForShowRandom_()
        {
            var list = _dbSet.Include(x => x.Products).ToList().Select(x => new CompanyShowViewModel
            {
                Id = x.Id,
                Name = x.Name,
                countProduct = x.Products.Count(),
                countHour = new TimeSpan(x.Products.Sum(r => r.Time.Ticks)),
                PrimaryImage = x.ImageLogo
            }).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            return list;
        }


        public List<string> GetNameForAutoComplete(string term)
        {
            return _dbSet.Where(x => x.Name.Contains(term)).OrderBy(x => Guid.NewGuid())
                .Select(x => x.Name).Take(10).ToList();

        }
    }
}
