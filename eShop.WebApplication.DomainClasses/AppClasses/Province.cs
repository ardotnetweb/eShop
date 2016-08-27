using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class Province
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام استان را وارد نمایید")]
        public string Name { get; set; }
        public virtual List<City> Cities { get; set; }

    }
}
