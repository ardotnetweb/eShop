using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.AppServices.InterfaceService
{
    public interface IProductService : IEntityService<Product>
    {
        int Count { get; }
        int CountOffer { get; }
        bool IsExistById(int id);
        int CountProductPerCompany(int id);
        int CountProductPerCategory(int id);




        ProductShowViewModel GetShowProductById(int id);
        ProductEditViewModel GetEditProductById(int id);
        ProductShowViewModel GetImageProductById(int id);
        List<ProductShowViewModel> GetDataTable(string term, int count, int page);
        List<ProductShowViewModel> GetDataTablePerCategory(int id, string term, int count, int page);
        List<ProductShowViewModel> GetDataTablePerCompany(int id, string term, int count, int page);
        List<ProductShowViewModel> GetDataTablePerCategoryForSearch(int? category_Id, int? company_Id, string term, int count, int page);
        List<ProductShowProfileViewModel> GetDataTableInterestUser(params int[] array);
        List<ProductShowViewModel> GetDataTableForOffer(int technology, string term, int count, int page);
        List<ProductShowViewModel> GetDataTableInOffer(int id);
        List<Product> GetAllProductByIdForOffer(params int[] array);


        //For Delete .. I Compuet Late
        bool GetByCompanyId(int Company_Id);

        //For Delete .. I Compuet Late
        bool GetByCategoryId(int Category_Id);



        //Compute it In Package Controller
        List<ProductShow_Create_Package_ViewModel> GetAllProductPerCategoryInPackage(int IdCategory, int count, int page);
        List<Product> GetAllByPackage(params int[] array);
        List<ProductShow_Create_Package_ViewModel> GetAllByPackage(int Category_Id, params int[] array);
        List<ProductShow_Create_Package_ViewModel> GetAllProductByPackage(params int[] array);


        //it have to go to company controller
        List<CompanyViewModel> GetAllCompanyWithCategoryId(int Category_Id);




        // These Codes for section forground :
        //this sinf used for section forground
        //4 محصول آخر را که درج شده اند دریافت کرده
        List<ProductShowViewModel> GetDataTableNew_();
        List<ProductShowViewModel> GetDataTableMostPopular_();
        List<ProductShowViewModel> GetdataTableProductOffer_();
        List<ProductShowViewModel> GetSixtennLastMoreVisited();
        List<ProductShowViewModel> GetdataTableProduct_(int[] array, int[] arrayCompany, string term, int count, int page, StatusSearch search = StatusSearch.MoreVisited,
            StatusOrder order = StatusOrder.Ascending);
        int CountProductSerach_(string term, int[] array, int[] arrayCompany);
        List<CompanySearchViewModel> GetDataTableNameCompany_(int id);
        List<ProductShowViewModel> GetSimilarProduct_(int id);
        List<ProductShowViewModel> GetAllProductPerCategoryByCompanyId_(int count, int page, int categoryId, int companyId);
        ProductShowViewModel_ GetProductById_(int id);
        List<BasketViewModel> GetAllProductInCompanyForAddToCart_(int id);
        void IncreaseVisit_(int id);
        bool CheckExistProductForAddToCart_(int id, double price);
        List<IntersetUserViewModel> GetAllFavoriteUser(params int[] array);
        ProductShowViewModel GetLastRecord();
        List<SitemapViewModel> GetLstItems();
        List<RssViewModel> GetLstItemsRss();
        List<RssViewModel> GetLstItemsCategoryRss(string name);

        ProductViewModelExamAddToCart GetByIdExamAddToCart(int id);



        List<string> GetNameForAutoComplete(string term);
    }
}
