using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.ViewModels
{
    public class RegisterViewModel
    {
        [Remote("CheckEmail", "Manage", ErrorMessage = "این ایمیل قبلا ثبت شده است")]
        [Required(ErrorMessage = "رایانامه را وارد نمایید")]
        [EmailAddress(ErrorMessage = "ایمیل را به صورت صحیح وارد نمایید")]
        [Display(Name = "رایا نامه")]
        public string Email { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد نمایید")]
        [StringLength(20, ErrorMessage = "حدئقل تعداد کاراکتر 6 و حداکثر 20 می باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "هر دو کلمه عبور باید یکسان باشند")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterViewModel_
    {
        [Remote("CheckEmail", "Account", ErrorMessage = "این ایمیل قبلا ثبت شده است")]
        [Required(ErrorMessage = "رایانامه را وارد نمایید")]
        [EmailAddress(ErrorMessage = "ایمیل را به صورت صحیح وارد نمایید")]
        [Display(Name = "رایا نامه")]
        public string Email { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد نمایید")]
        [StringLength(20, ErrorMessage = "حدئقل تعداد کاراکتر 6 و حداکثر 20 می باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "هر دو کلمه عبور باید یکسان باشند")]
        public string ConfirmPassword { get; set; }
    }





}

