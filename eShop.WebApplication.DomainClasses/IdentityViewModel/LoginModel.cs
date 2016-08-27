using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.IdentityViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "رایانامه را وارد نمایید")]
        [EmailAddress(ErrorMessage = "ایمیل را به صورت صحیح وارد نمایید")]
        [Display(Name = "رایا نامه")]
        public string Email { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد نمایید")]
        [StringLength(20, ErrorMessage = "حدئقل تعداد کاراکتر 6 و حداکثر 20 می باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به یاد داشته باش")]
        public bool RememberMe { get; set; }

        
    }
}
