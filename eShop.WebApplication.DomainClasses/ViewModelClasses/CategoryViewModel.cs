using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{

    public class CategoryViewModel
    {
        [Required(ErrorMessage = "نام گروه را وارد نمایید", AllowEmptyStrings = false)]
        [Display(Name = "نام گروه")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 20, MinimumLength = 3,
            ErrorMessage = "حدئقل 4 کاراکتر و حداکثر 20 کاراکتر")]
        public string Name { get; set; }


        [Display(Name = "ریشه گروه")]
        [Required(ErrorMessage = "یکی از گروه ها را انتخاب نمایید", AllowEmptyStrings = false)]
        public int Parent { get; set; }

        public int Id { get; set; }

        public string ParentName { get; set; }
    }
}
