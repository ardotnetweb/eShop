using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class Label
    {
        public Label()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        [Display(Name = "نام برچسب")]
        [Required(ErrorMessage = "نام برچسب را وارد نمایید")]
        [StringLength(maximumLength: 30, MinimumLength = 2,
        ErrorMessage = "نام برچسب باید بین 2 کاراکتر تا 30 کاراکتر باشد")]
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
