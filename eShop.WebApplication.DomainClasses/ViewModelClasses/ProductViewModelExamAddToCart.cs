using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ProductViewModelExamAddToCart
    {
        public int Id { get; set; }
        public string PrimaryImage { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

}
