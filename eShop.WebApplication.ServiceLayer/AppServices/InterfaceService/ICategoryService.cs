using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.MainViewModel;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface ICategoryService : IEntityService<Category>
    {
        int Count { get; }
        int CountRoot { get; }
        int CountSub { get; }
        bool IsExistRoot(int id);
        bool IsaExistByName(string name);
        bool IsExistById(int id);
        bool CheckCollectionId_(params int[] arrayCategory);
        ExistNameCategory GetByName(object name);
        Category GetByName(string name);
        CategoryViewModel GetById_(int id);
        CategoryViewModel GetRandom_();
        List<Category> GetDataTable(string term, int count, int page);
        /// <summary>
        /// Same With method GetAllParentForDropdownwith result diffrence
        /// </summary>
        /// <returns></returns>
        List<Category> GetAllRoot();
        /// <summary>
        /// Same With method GetAllSubForDropdown with result diffrence
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Category> GetAllSub(int id);
        List<CategoryViewModel> GetAllSub_(int id);
        List<SubCategoryDropListViewModel> GetAllSubForDropdown();
        List<ParentCategoryDropListViewModel> GetAllParentForDropdown();
        List<ClassifiedProduct> GetAllCountProductPer();
        //the method GetAllSub and the method GetAllSubForDropdown are same but result diffrence        
        //the method GetAllRoot and the method GetAllParentForDropdown are same but result diffrence



        //use forground : section all user 
        List<CategoryMenuViewModel> GetAllCategoryForMenu_();



        List<string> GetNameForAutoComplete(string term);

    }
}
