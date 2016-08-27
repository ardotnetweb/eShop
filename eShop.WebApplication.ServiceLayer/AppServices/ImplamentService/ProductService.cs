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
using eShop.WebApplication.DomainClasses.Utility;

namespace eShop.WebApplication.ServiceLayer.AppServices.ImplamentService
{
    public class ProductService : EntityService<Product>, IProductService
    {
        private readonly IDbSet<Product> _dbSet;
        private IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Product>();

        }
        public int Count { get { return _dbSet.Count(); } }
        public int CountProductPerCategory(int id)
        {
            var count = _dbSet.Where(x => x.Category.Id == id && x.Recomend == 1).Count();
            return count;
        }
        public bool IsExistById(int id)
        {
            return _dbSet.Any(x => x.Id == id);
        }
        public int CountProductPerCompany(int id)
        {
            return _dbSet.AsQueryable().Where(x => x.Company.Id == id).Count();
        }
        public ProductShowViewModel GetShowProductById(int id)
        {
            return _dbSet.AsQueryable().Include(x => x.Labels).Include(x => x.ImageProducts).Where(x => x.Id == id).
                Select(product => new ProductShowViewModel
                {
                    Category_Name = product.Category.Name,
                    Company_Name = product.Company.Name,
                    Date = product.Date,
                    Time = product.Time,
                    PrimaryImage = product.PrimryImage,
                    Labels = product.Labels,
                    Id = product.Id,
                    Name = product.Name,
                    ImageProducts = product.ImageProducts,
                    Explain = product.Explain,
                    Recomend = product.Recomend,
                    Price = product.Price

                }).FirstOrDefault();
        }
        public ProductEditViewModel GetEditProductById(int id)
        {
            return _dbSet.AsQueryable().Include(x => x.Labels).Include(x => x.ImageProducts).Where(x => x.Id == id).Select(product =>
                new ProductEditViewModel
                {
                    Id = product.Id,
                    Price = product.Price,
                    Category_Id = product.Category.Id,
                    Company_Id = product.Company.Id,
                    Date = product.Date,
                    Time = product.Time,
                    Explain = product.Explain,
                    ImageProducts = product.ImageProducts,
                    PrimaryImage = product.PrimryImage,
                    Labels = product.Labels,
                    Name = product.Name
                }).FirstOrDefault();
        }
        public ProductShowViewModel GetImageProductById(int id)
        {
            return _dbSet.AsQueryable().Include(x => x.ImageProducts).Where(x => x.Id == id).Select(product => new ProductShowViewModel
            {
                Category_Name = product.Category.Name,
                Company_Name = product.Company.Name,
                Id = product.Id,
                Name = product.Name,
                ImageProducts = product.ImageProducts,
            }).FirstOrDefault();
        }
        public List<ProductShowViewModel> GetDataTable(string term, int count, int page)
        {
            var list = _dbSet.AsQueryable<Product>().Where(x => x.Name.Contains(term)).
                Select(product => new ProductShowViewModel
                {
                    Company_Name = product.Company.Name,
                    Category_Name = product.Category.Name,
                    Date = product.Date,
                    // Labels = product.Labels,
                    Name = product.Name,
                    Time = product.Time,
                    Id = product.Id
                })
            .OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();
            return list;
        }
        public List<ProductShowViewModel> GetDataTablePerCategory(int Id, string term, int count, int page)
        {
            var list = _dbSet.AsQueryable<Product>().Where(x => x.Category.Id == Id).
                Select(product => new ProductShowViewModel
                {
                    Company_Name = product.Company.Name,
                    Category_Name = product.Category.Name,
                    Date = product.Date,
                    // Labels = product.Labels,
                    Name = product.Name,
                    Time = product.Time,
                    Id = product.Id
                })
            .OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();
            return list;
        }
        public List<ProductShowViewModel> GetDataTablePerCompany(int id, string term, int count, int page)
        {
            var list = _dbSet.AsQueryable<Product>().Where(x => x.Company.Id == id).
                Select(product => new ProductShowViewModel
                {
                    Category_Name = product.Category.Name,
                    Date = product.Date,
                    //Labels = product.Labels,
                    Name = product.Name,
                    Id = product.Id
                })
            .OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();
            return list;
        }
        public List<ProductShowViewModel> GetDataTablePerCategoryForSearch(int? category_Id, int? company_Id, string term, int count, int page)
        {
            if (category_Id != null && company_Id != null)
            {
                var list = _dbSet.AsQueryable<Product>().Where(x => x.Name.Contains(term) &&
                    x.Company.Id == company_Id && x.Category.Id == category_Id).
                     Select(product => new ProductShowViewModel
                     {
                         Company_Name = product.Company.Name,
                         Category_Name = product.Category.Name,
                         Name = product.Name,
                         Id = product.Id
                     })
                 .OrderBy(x => x.Id).Skip(page * count).Take(page).ToList();
                return list;
            }
            else if (category_Id != null && company_Id == null)
            {
                var list = _dbSet.AsQueryable<Product>().Where(x => x.Name.Contains(term) &&
                    x.Category.Id == category_Id).
                     Select(product => new ProductShowViewModel
                     {
                         Company_Name = product.Company.Name,
                         Category_Name = product.Category.Name,
                         Name = product.Name,
                         Id = product.Id
                     })
                 .OrderBy(x => x.Id).Skip(page * count).Take(page).ToList();
                return list;
            }
            else if (category_Id == null && company_Id != null)
            {
                var list = _dbSet.AsQueryable<Product>().Where(x => x.Name.Contains(term) &&
                    x.Company.Id == company_Id).
                     Select(product => new ProductShowViewModel
                     {
                         Company_Name = product.Company.Name,
                         Category_Name = product.Category.Name,
                         Name = product.Name,
                         Id = product.Id
                     })
                 .OrderBy(x => x.Id).Skip(page * count).Take(page).ToList();
                return list;
            }
            else
            {
                var list = _dbSet.AsQueryable<Product>().Where(x => x.Name.Contains(term)).
                     Select(product => new ProductShowViewModel
                     {
                         Company_Name = product.Company.Name,
                         Category_Name = product.Category.Name,
                         Name = product.Name,
                         Id = product.Id
                     })
                 .OrderBy(x => x.Id).Skip(page * count).Take(page).ToList();
                return list;
            }


        }
        public List<ProductShowProfileViewModel> GetDataTableInterestUser(params int[] array)
        {
            return _dbSet.AsQueryable<Product>().Where(x => array.Contains(x.Id))
                .Select(x => new ProductShowProfileViewModel
                {
                    Category_Name = x.Category.Name,
                    Company_Id = x.Company.Id,
                    Company_Name = x.Company.Name,
                    Date = x.Date,
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }
        public List<ProductShowViewModel> GetDataTableForOffer(int technology, string term, int count, int page)
        {
            var list = _dbSet.AsQueryable<Product>().Where(x => x.Name.Contains(term) && x.Category.Id == technology && x.Recomend == 1).
                    Select(product => new ProductShowViewModel
                    {
                        Company_Name = product.Company.Name,
                        Category_Name = product.Category.Name,
                        Date = product.Date,
                        Name = product.Name,
                        Time = product.Time,
                        Id = product.Id,
                        Price = product.Price
                    })
                .OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();
            return list;
        }






        public ProductEditViewModel GetByIdEdit(int Id)
        {
            return _dbSet.Include(x => x.ImageProducts).Include(x => x.Labels).AsQueryable().Where(x => x.Id == Id).Select(product =>
                new ProductEditViewModel
                {
                    Id = product.Id,
                    Price = product.Price,
                    Category_Id = product.Category.Id,
                    Company_Id = product.Company.Id,
                    Date = product.Date,
                    Time = product.Time,
                    Explain = product.Explain,
                    ImageProducts = product.ImageProducts,
                    PrimaryImage = product.PrimryImage,
                    Labels = product.Labels,
                    Name = product.Name
                }).FirstOrDefault();
        }
        public bool GetByCompanyId(int Company_Id)
        {
            return _dbSet.Any(x => x.Company.Id == Company_Id);
        }
        public bool GetByCategoryId(int Category_Id)
        {
            return _dbSet.Any(x => x.Category.Id == Category_Id);
        }
        public List<CompanyViewModel> GetAllCompanyWithCategoryId(int Category_Id)
        {
            var list = _dbSet.AsQueryable().Where(x => x.Category.Id == Category_Id)
                 .Select(company => new CompanyViewModel
                 {
                     IdCompany = company.Company.Id,
                     ImageCompany = company.Company.ImageLogo,
                     NameCompany = company.Company.Name,
                     TitleCompany = company.Company.Title
                 }).Distinct().ToList();
            return list;
        }
        public List<ProductShow_Create_Package_ViewModel> GetAllProductPerCategoryInPackage(int IdCategory, int count, int page)
        {
            var list = _dbSet.AsQueryable<Product>().Where(x => x.Category.Id == IdCategory).
                Select(product => new ProductShow_Create_Package_ViewModel
                {
                    Company_Name = product.Company.Name,
                    Name = product.Name,
                    Time = product.Time,
                    Id = product.Id,
                    Price = product.Price,
                    Category_Id = product.Category.Id
                })
            .OrderBy(x => x.Id).Skip(page * count).Take(count).ToList();
            return list;
        }
        public List<Product> GetAllByPackage(params int[] array)
        {

            return _dbSet.AsQueryable<Product>().Where(x => array.Contains(x.Id)).ToList();
        }
        public List<ProductShow_Create_Package_ViewModel> GetAllByPackage(int Category_Id, params int[] array)
        {

            return _dbSet.AsQueryable<Product>().Where(x => x.Category.Id == Category_Id).Where(x => !array.Contains(x.Id))
                .Select(x => new ProductShow_Create_Package_ViewModel
                {
                    Category_Id = x.Category.Id,
                    Company_Name = x.Company.Name,
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Time = x.Time
                }).ToList();

        }
        public List<ProductShow_Create_Package_ViewModel> GetAllProductByPackage(params int[] array)
        {
            return _dbSet.AsQueryable<Product>().Where(x => array.Contains(x.Id))
                  .Select(x => new ProductShow_Create_Package_ViewModel
                  {
                      Category_Id = x.Category.Id,
                      Company_Name = x.Company.Name,
                      Id = x.Id,
                      Name = x.Name,
                      Price = x.Price,
                      Time = x.Time
                  }).ToList();
        }


        public List<Product> GetAllProductByIdForOffer(params int[] array)
        {
            return _dbSet.Where(x => array.Contains(x.Id)).ToList();
        }







        public List<ProductShowViewModel> GetDataTableInOffer(int id)
        {
            return _dbSet.Where(x => x.Category.Id == id && x.Recomend == 2)
                .Select(product => new ProductShowViewModel
                {
                    Company_Name = product.Company.Name,
                    Category_Name = product.Category.Name,
                    Date = product.Date,
                    Name = product.Name,
                    Time = product.Time,
                    Id = product.Id,
                    Price = product.Price
                }).ToList();
        }

        //for section for ground site

        public List<ProductShowViewModel> GetDataTableNew_()
        {
            return _dbSet.AsNoTracking().Select(x => new ProductShowViewModel
            {
                Id = x.Id,
                PrimaryImage = x.PrimryImage,
                Name = x.Name,
                Date = x.Date

            }).OrderBy(x => Guid.NewGuid()).OrderByDescending(x => x.Id).Take(4).ToList();
        }


        public List<ProductShowViewModel> GetDataTableMostPopular_()
        {
            return _dbSet.AsNoTracking().Select(x => new ProductShowViewModel
            {
                Id = x.Id,
                PrimaryImage = x.PrimryImage,
                Name = x.Name,
                Date = x.Date,
                CountVisit = x.VisitNumber

            }).OrderBy(x => Guid.NewGuid()).OrderBy(x => x.CountVisit).Take(8).ToList();
        }


        public List<ProductShowViewModel> GetdataTableProductOffer_()
        {
            return _dbSet.AsNoTracking().Where(x => x.Recomend == 2).Select(x => new ProductShowViewModel
            {
                Id = x.Id,
                PrimaryImage = x.PrimryImage,
                Name = x.Name,
                Date = x.Date

            }).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
        }
        public List<ProductShowViewModel> GetdataTableProduct_(int[] array, int[] arrayCompany, string term, int count, int page,
            StatusSearch search = StatusSearch.MoreVisited,
            StatusOrder order = StatusOrder.Ascending)
        {
            var lstProduct = _dbSet.Where(x => array.Contains(x.Category.Id))
               .Where(x => arrayCompany.Contains(x.Company.Id))
               .Where(x => x.Name.Contains(term)).AsQueryable();



            if (order == StatusOrder.Ascending)
            {
                switch (search)
                {
                    case StatusSearch.MoreVisited:
                        lstProduct = lstProduct.OrderByDescending(x => x.VisitNumber).AsQueryable();
                        break;
                    case StatusSearch.New:
                        lstProduct = lstProduct.OrderByDescending(x => x.Date).AsQueryable();
                        break;
                    case StatusSearch.Offer:
                        lstProduct = lstProduct.OrderBy(x => x.Recomend == (int)StatusRecomend.SpecialRecomend).AsQueryable();
                        break;
                    case StatusSearch.Price:
                        lstProduct = lstProduct.OrderByDescending(x => x.Price).AsQueryable();
                        break;
                }
            }
            else
            {
                switch (search)
                {
                    case StatusSearch.MoreVisited:
                        lstProduct = lstProduct.OrderBy(x => x.VisitNumber).AsQueryable();
                        break;
                    case StatusSearch.New:
                        lstProduct = lstProduct.OrderBy(x => x.Id).AsQueryable();
                        break;
                    case StatusSearch.Offer:
                        lstProduct = lstProduct.OrderByDescending(x => x.Recomend == (int)StatusRecomend.SpecialRecomend).AsQueryable();
                        break;
                    case StatusSearch.Price:
                        lstProduct = lstProduct.OrderBy(x => x.Price).AsQueryable();
                        break;

                }
            }

            return lstProduct.Select(x => new ProductShowViewModel
              {
                  Id = x.Id,
                  Name = x.Name,
                  Price = x.Price,
                  PrimaryImage = x.PrimryImage,
                  Explain = (x.Explain.Length > 380 ? x.Explain.Substring(0, 380) : x.Explain)
              }).Skip(count * page).Take(count).ToList();

        }


        public int CountProductSerach_(string term, int[] array, int[] arrayCompany)
        {
            return _dbSet.Where(x => array.Contains(x.Category.Id))
                .Where(x => arrayCompany.Contains(x.Company.Id)).
                Where(x => x.Name.Contains(term)).Count();
        }


        public List<CompanySearchViewModel> GetDataTableNameCompany_(int id)
        {
            var query = _dbSet.Where(x => x.Category.Parent.Id == id).AsQueryable();
            var list = query.Select(x => new CompanySearchViewModel
            {
                Id = x.Company.Id,
                Name = x.Company.Name
            }).DistinctBy(x => x.Name).ToList();
            return list;
        }


        public List<ProductShowViewModel> GetSimilarProduct_(int id)
        {
            return _dbSet.Where(x => x.Category.Parent.Id == id).OrderBy(x => Guid.NewGuid())
               .Select(x => new ProductShowViewModel
               {
                   PrimaryImage = x.PrimryImage,
                   Id = x.Id,
                   Name = x.Name
               }).Take(12).ToList();
        }



        public List<ProductShowViewModel> GetAllProductPerCategoryByCompanyId_(int count, int page, int categoryId, int companyId)
        {
            return _dbSet.Where(x => x.Category.Id == categoryId).Where(x => x.Company.Id == companyId)
                .Select(x => new ProductShowViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Time = x.Time
                }).OrderBy(x => x.Id).Skip(page * count)
                .Take(count).ToList();
        }


        public ProductShowViewModel_ GetProductById_(int id)
        {
            return _dbSet.Include(x => x.ImageProducts).Include(x => x.Labels)
                .Where(x => x.Id == id).Select(x => new ProductShowViewModel_
            {
                Category = new CategoryDropListViewModel_ { Category_Id = x.Category.Id, Name = x.Category.Name, Parent_Id = x.Category.Parent.Id },
                Company = new CompanyDropListViewModel { Company_Id = x.Company.Id, Name = x.Company.Name },
                CountVisit = x.VisitNumber,
                Date = x.Date,
                Time = x.Time,
                Explain = x.Explain,
                ImageProducts = x.ImageProducts,
                Labels = x.Labels,
                Name = x.Name,
                Id = x.Id,
                Price = x.Price,
                PrimaryImage = x.PrimryImage,
                Recomend = x.Recomend
            }).FirstOrDefault();
        }


        public List<BasketViewModel> GetAllProductInCompanyForAddToCart_(int id)
        {
            return _dbSet.Where(x => x.Company.Id == id).Select(x => new BasketViewModel
            {
                Name = x.Name,
                Price = x.Price,
                Product_Id = x.Id,
                Quantity = 1,
                StatusTypeOrder = StatusTypeOrder.EWCO
            }).ToList();
        }


        public void IncreaseVisit_(int id)
        {
            var product = _dbSet.Where(x => x.Id == id).FirstOrDefault();
            product.VisitNumber += 1;
            _unitOfWork.Entry(product).State = EntityState.Modified;
            _unitOfWork.SaveAllChanges();
        }


        public bool CheckExistProductForAddToCart_(int id, double price)
        {
            return _dbSet.Any(x => x.Id == id && x.Price == price);
        }


        public List<IntersetUserViewModel> GetAllFavoriteUser(params int[] array)
        {
            return _dbSet.AsQueryable<Product>().Where(x => array.Contains(x.Id))
                      .Select(x => new IntersetUserViewModel
                      {
                          Category_Id = x.Category.Id,
                          Category_Name = x.Category.Name,
                          Company_Name = x.Company.Name,
                          Company_Id = x.Company.Id,
                          Id = x.Id,
                          Name = x.Name,
                          Price = x.Price
                      }).ToList();
        }


        public List<ProductShowViewModel> GetSixtennLastMoreVisited()
        {
            return _dbSet.Select(x => new ProductShowViewModel
            {
                PrimaryImage = x.PrimryImage,
                Name = x.Name.Length > 45 ? x.Name.Substring(0, 45) : x.Name,
                Id = x.Id
            }).
                OrderBy(x => x.CountVisit).OrderByDescending(x => Guid.NewGuid()).Take(16).ToList();
        }


        public ProductShowViewModel GetLastRecord()
        {
            return _dbSet.OrderByDescending(x => x.Id).
                Select(x => new ProductShowViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefault();
        }


        public List<SitemapViewModel> GetLstItems()
        {
            return _dbSet.AsNoTracking().Select(x => new SitemapViewModel
            {
                ChangeFrequency = eChangeFrequency.weekly,
                ModifiedDate = x.ModifiedDate,
                Priority = Priority.priority_05,
                Product_Id = x.Id,
                Product_Name = x.Name
            })
                .OrderByDescending(x => x.ModifiedDate).Take(20).ToList();
        }



        public List<RssViewModel> GetLstItemsRss()
        {
            return _dbSet.AsNoTracking().Select(x => new RssViewModel
            {
                Date = x.Date,
                Explain = x.Explain,
                Product_Id = x.Id,
                Product_Name = x.Name
            }).OrderByDescending(x => x.Date)
                .Take(50).ToList();
        }

        public List<RssViewModel> GetLstItemsCategoryRss(string name)
        {
            var list = _dbSet.Include(x => x.Category).Where(x => x.Category.Name.Replace(" ", string.Empty)
               .ToLower().Equals(name.ToLower()))
                .Select(x => new RssViewModel
                {
                    Date = x.Date,
                    Explain = x.Explain,
                    Product_Id = x.Id,
                    Product_Name = x.Name
                }).Take(50).ToList();
            return list;
        }


        public ProductViewModelExamAddToCart GetByIdExamAddToCart(int id)
        {
            return _dbSet.Where(x => x.Id == id).Select(x => new ProductViewModelExamAddToCart
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                PrimaryImage = x.PrimryImage
            }).FirstOrDefault();
        }


        public int CountOffer
        {
            get { return _dbSet.Where(x => x.Recomend == 2).Count(); }
        }


        public List<string> GetNameForAutoComplete(string term)
        {
            return _dbSet.Where(x => x.Name.Contains(term)).Select(x => x.Name)
                .OrderBy(x => Guid.NewGuid()).Take(10).ToList();
        }
    }
}

