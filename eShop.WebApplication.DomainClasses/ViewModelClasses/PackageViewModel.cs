using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;
using System.Web.Mvc;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class PackageViewModel
    {
        public int Id { get; set; }
        public string NamePackage { get; set; }
        public string ImagePackage { get; set; }
        public int PercentPackage { get; set; }
        public double OriginalPricePackage { get; set; }

        public double DisCountPricePackage { get; set; }
        public virtual int ProductsPackage { get; set; }

        [UIHint("DisplayPersianDatePicker")]
        public DateTime StartDatePackage { get; set; }

        [UIHint("DisplayPersianDatePicker")]
        public DateTime EndDatePackage { get; set; }
        public bool IsShowPackage { get; set; }
        public virtual string TimeEducationPackage { get; set; }
        [AllowHtml]
        public virtual string ExplainPackage { get; set; }
        public virtual Category Category { get; set; }
    }

    public class PackageShowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Explain { get; set; }
        public string Image { get; set; }
    }
}
