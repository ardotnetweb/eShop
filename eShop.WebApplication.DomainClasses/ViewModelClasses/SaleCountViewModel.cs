using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class SaleCountViewModel
    {
        public int Confirmed { get; set; }
        public int NotConfirmed { get; set; }
        public int Count { get; set; }
    }
}
