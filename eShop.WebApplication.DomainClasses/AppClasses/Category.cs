
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class Category
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "نام گروه را وارد نمایید")]
        [Display(Name = "نام گروه")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "حدئقل 3 کاراکتر و حداکثر 20 کاراکتر")]
        public virtual string Name { get; set; }

        [Display(Name = "ریشه گروه")]
        public  virtual Category Parent { get; set; }

        public virtual List<Product> Products { get; set; }
        public virtual List<Package> Packages { get; set; }
    }
}
