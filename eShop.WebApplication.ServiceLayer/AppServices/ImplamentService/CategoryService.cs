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
    public class CategoryService : EntityService<Category>, ICategoryService
    {
        private readonly IDbSet<Category> _dbSet;
        private IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._dbSet = _unitOfWork.Set<Category>();
        }
        public int Count { get { return _dbSet.Count(); } }
        public int CountRoot { get { return _dbSet.Where(x => x.Parent == null).Count(); } }
        public int CountSub { get { return _dbSet.Where(x => x.Parent != null).Count(); } }
        public bool IsExistById(int id)
        {
            return _dbSet.Any(x => x.Id == id);
        }
        public bool IsExistRoot(int id)
        {
            return _dbSet.Any(x => x.Parent.Id == id);
        }
        public bool IsaExistByName(string name)
        {
            return _dbSet.Any(x => x.Name.Equals(name));
        }
        public Category GetByName(string name)
        {
            return _dbSet.Where(category => category.Name.Equals(name)).FirstOrDefault();
        }
        public List<Category> GetDataTable(string term, int count, int page)
        {
            var list = _dbSet.AsQueryable<Category>().Where(x => x.Name.Contains(term))
                .OrderBy(x => x.Id).Skip(page * count).Take(page).ToList();

            return list;
        }
        public List<Category> GetAllRoot()
        {
            return _dbSet.Where(x => x.Parent == null).ToList(); ;
        }
        public List<Category> GetAllSub(int id)
        {
            return _dbSet.Where(x => x.Parent.Id == id).ToList();
        }
        public List<SubCategoryDropListViewModel> GetAllSubForDropdown()
        {
            List<SubCategoryDropListViewModel> list = _dbSet.
                Where(category => category.Parent != null).Select(x => new SubCategoryDropListViewModel { Category_Id = x.Id, Name = x.Name }).ToList();
            return list;
        }
        public List<ClassifiedProduct> GetAllCountProductPer()
        {
            var list = _dbSet.AsQueryable().Where(x => x.Parent.Id != null).Select
                (category => new ClassifiedProduct
                {
                    Name = category.Name,
                    NumberProduct = category.Products.Count(),
                    Id = category.Id
                }).OrderByDescending(x => x.NumberProduct);
            return list.ToList();
        }


        public List<ParentCategoryDropListViewModel> GetAllParentForDropdown()
        {
            return _dbSet.Where(x => x.Parent == null).Select(x =>
                new ParentCategoryDropListViewModel
                {
                    Name = x.Name,
                    Parent_Id = x.Id
                }).ToList(); ;
        }


        public ExistNameCategory GetByName(object name)
        {
            return _dbSet.Where(category => category.Name.Equals(name.ToString())).
                Select(x => new ExistNameCategory
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefault();
        }



        public List<CategoryMenuViewModel> GetAllCategoryForMenu_()
        {

            var listResult = new List<CategoryMenuViewModel>();
            //var parent = _dbSet.Where(x => x.Parent.Id == null).ToList();

            //foreach (var item in parent)
            //{
            //    var sample = new CategoryMenuViewModel { Name = item.Name };
            //    sample.Category = _dbSet.Where(x => x.Parent.Id == item.Id).ToList();
            //    listResult.Add(sample);
            //}

            var list = _dbSet.ToList();
            var parentList = list.Where(x => x.Parent == null).ToList();
            var childreenList = list.Where(x => x.Parent != null).ToList();
            foreach (var item in parentList)
            {
                var sample=new CategoryMenuViewModel { Name = item.Name };
                sample.Category = childreenList.Where(x => x.Parent.Id == item.Id).ToList();
                listResult.Add(sample);
            }
            return listResult;
        }


        public CategoryViewModel GetById_(int id)
        {
            return _dbSet.Where(x => x.Id == id).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Parent = x.Parent.Id,
                ParentName = x.Parent.Name
            }).FirstOrDefault();
        }

        public List<CategoryViewModel> GetAllSub_(int id)
        {
            return _dbSet.Where(x => x.Parent.Id == id).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Parent = x.Parent.Id
            }).ToList();
        }


        public CategoryViewModel GetRandom_()
        {
            return _dbSet.Where(x => x.Parent.Id != null).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Parent = x.Parent.Id,
                ParentName = x.Parent.Name
            }).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }


        public bool CheckCollectionId_(params int[] arrayCategory)
        {
            bool result = true;
            if (arrayCategory != null && arrayCategory.Length > 0)
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





        public List<string> GetNameForAutoComplete(string term)
        {
            return _dbSet.Where(x => x.Name.Contains(term)).Select(x => x.Name)
                .OrderBy(x => Guid.NewGuid()).Take(20).ToList();
        }
    }
}