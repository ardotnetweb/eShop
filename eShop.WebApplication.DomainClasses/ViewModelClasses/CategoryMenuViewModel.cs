using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class CategoryMenuViewModel
    {
        public string Name { get; set; }
        public List<Category> Category { get; set; }
    }
}
