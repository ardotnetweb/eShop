using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eShop.WebApplication.ServiceLayer.AppServices.ImplamentService
{
    public class PackageService : EntityService<Package>, IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDbSet<Package> _dbSet;
        public PackageService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._dbSet = _unitOfWork.Set<Package>();
        }

        public bool IsExistByName(string name)
        {
            return _dbSet.Any(x => x.Name.Equals(name));
        }


        public List<PackageViewModel> GetAllPackageByPaging(string term, int count, int page)
        {
            var list = _dbSet.Include(x => x.Products).AsQueryable<Package>().Where(x => x.Name.Contains(term))
                .Select(x => new PackageViewModel
                {
                    Id = x.Id,
                    NamePackage = x.Name,
                    PercentPackage = x.Percent,
                    ImagePackage = x.Image,
                    IsShowPackage = x.IsShow,
                    DisCountPricePackage = x.DisCountPrice,
                    OriginalPricePackage = x.OriginalPrice,
                    StartDatePackage = x.StartDate,
                    EndDatePackage = x.EndDate,
                    ProductsPackage = x.Products.Count()
                }).OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();

            return list;

        }


        public PackageViewModel GetPackageByIdPackage(int id)
        {
            var list = _dbSet.Include(x => x.Products).AsQueryable().Where(x => x.Id == id).Select
                (x => new PackageViewModel
                {
                    Id = x.Id,
                    NamePackage = x.Name,
                    PercentPackage = x.Percent,
                    OriginalPricePackage = x.OriginalPrice,
                    DisCountPricePackage = x.DisCountPrice,
                    ProductsPackage = x.Products.Count(),
                    EndDatePackage = x.EndDate,
                    StartDatePackage = x.StartDate,
                    IsShowPackage = x.IsShow,
                    ImagePackage = x.Image,
                    TimeEducationPackage = x.TimeEducation,
                    ExplainPackage = x.Explain,
                    Category = x.Category
                }).FirstOrDefault();
            return list;
        }


        public List<ProductPackageViewModel> GetProductPerPackage(int id)
        {
            var list = new List<ProductPackageViewModel>();
            var productList = _dbSet.Where(x => x.Id == id)
                .Select(x => x.Products).ToList();

            foreach (var item in productList)
            {
                foreach (var product in item)
                {
                    list.Add(new ProductPackageViewModel
                    {
                        Id = product.Id,
                        Category_Name = product.Category.Name,
                        PricePackage = product.Price,
                        PrimaryImage = product.PrimryImage,
                        Name = product.Name,
                        Date = product.Date,
                        Time = product.Time
                    });
                }
            }
            return list;
        }


        public Package GetByName(string name)
        {
            return _dbSet.Where(x => x.Name.Equals(name)).FirstOrDefault();
        }


        public List<PackageShowViewModel> GetManyPackage_()
        {
            return _dbSet.Select(x => new PackageShowViewModel
            {
                Id = x.Id,
                Explain = x.Explain,
                Image = x.Image,
                Name = x.Name.Length > 30 ? x.Name.Substring(0, 30) : x.Name
            }).OrderBy(x => Guid.NewGuid()).Take(3).ToList();
        }

        public int Count
        {
            get { return _dbSet.Count(); }
        }

        public int CountActive
        {
            get { return _dbSet.Where(x => x.IsShow == true).Count(); }
        }

        public int CountDisable
        {
            get { return _dbSet.Where(x => x.IsShow == false).Count(); }
        }


        public void ToDoDisablePackage()
        {
            var list = _dbSet.Where(x => x.EndDate > DateTime.Now).ToList();
            list.ForEach(x =>
            {
                x.IsShow = false;
            });

            foreach (var item in list)
                this.Update(item);

            _unitOfWork.SaveAllChanges();
        }
    }

}
