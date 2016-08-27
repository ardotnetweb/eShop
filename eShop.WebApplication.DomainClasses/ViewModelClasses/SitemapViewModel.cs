using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class SitemapViewModel
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public eChangeFrequency ChangeFrequency { get; set; }
        public DateTime ModifiedDate { get; set; }
        public double Priority { get; set; }
    }
}
