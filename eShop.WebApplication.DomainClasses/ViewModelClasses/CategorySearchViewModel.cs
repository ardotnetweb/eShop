using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class CategorySearchViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public int CategoryId { get; set; }
        public string ParentName { get; set; }
        public List<CompanySearchViewModel> Companies { get; set; }
    }
}
