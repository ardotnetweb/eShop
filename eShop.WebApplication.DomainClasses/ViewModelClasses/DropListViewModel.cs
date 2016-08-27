using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class SubCategoryDropListViewModel
    {
        [Required(ErrorMessage = "یکی از گروه های آموزشی را انتخاب نمایید")]
        public int Category_Id { get; set; }
        public string Name { get; set; }
    }
    public class ParentCategoryDropListViewModel
    {
        public string Name { get; set; }
        public int Parent_Id { get; set; }
    }


    public class CompanyDropListViewModel
    {
        [Required(ErrorMessage = "یکی از کمپانی های سازنده محصولات را انتخاب نمایید")]
        public int Company_Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryDropListViewModel_
    {
        public int Category_Id { get; set; }
        public string Name { get; set; }
        public int Parent_Id { get; set; }
    }
    
}
