using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ClasseListTechnology
    {
        public int TotalProduct { get; set; }
        public List<ClassifiedProduct> categories { get; set; }

    }
    public class ClassifiedProduct
    {
        public int NumberProduct { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
