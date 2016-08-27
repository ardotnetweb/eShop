using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class CompanyViewModel
    {
        public int IdCompany { get; set; }
        public string NameCompany { get; set; }
        public string TitleCompany { get; set; }
        public string ImageCompany { get; set; }
        public string ExplainCompany { get; set; }
        public string AddressCompany { get; set; }
    }
    public class CompanyShowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int countProduct { get; set; }
        public TimeSpan countHour { get; set; }
        public string PrimaryImage { get; set; }
    }
    public class CompanySearchViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class CompanyInformationToCart
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
