using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class RssViewModel
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public DateTime Date { get; set; }
        public string Explain { get; set; }
    }
}
