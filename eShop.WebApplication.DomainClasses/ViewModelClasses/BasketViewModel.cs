using eShop.WebApplication.DomainClasses.AppClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Product_Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public StatusTypeOrder StatusTypeOrder { get; set; }
        public string PrimaryImage { get; set; }
    }
}
