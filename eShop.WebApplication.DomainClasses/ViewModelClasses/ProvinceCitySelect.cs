using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ProvinceShowViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="لطفا نام استان را وارد نمایید")]
        [Display(Name="استان")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }

    public class CityShowViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "لطفا نام استان را وارد نمایید")]
        [Display(Name = "استان")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }


}
