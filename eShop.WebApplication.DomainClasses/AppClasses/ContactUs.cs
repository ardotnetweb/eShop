using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Explain { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User_Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int User_Id { get; set; }
        public bool StatusRead { get; set; }
        public bool StatusAnswer { get; set; }
    }
    public class ContactUsViewModel
    {
        [Display(Name = "نام")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "نام شما باید بین 3 کاراکتر تا 20 کاراکتر باشد")]
        [Required(ErrorMessage = "نام خود را وارد نمایید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "نام خانوادگی شما باید بین 3 کاراکتر تا 20 کاراکتر باشد")]
        [Required(ErrorMessage = "نام خانوادگی خود را وارد نمایید")]
        public string Family { get; set; }

        [Display(Name = "شماره موبایل")]
        [StringLength(11, ErrorMessage = "شماره همراه شما شامل 11 کاراکتر می شود")]
        [Required(ErrorMessage = "شماره موبایل خود را وارد نمایید")]
        public string Phone { get; set; }


        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "متن پیام")]
        [StringLength(2500, MinimumLength = 25, ErrorMessage = "نام شما باید بین 25 کاراکتر تا 2500 کاراکتر باشد")]
        [Required(ErrorMessage = "متن پیام خود را ئارد نمایید")]
        public string Explain { get; set; }


        [Display(Name = "عبارت تصویر")]
        [Required(ErrorMessage = "مقدار تصویر امنیتی را وارد نمایید/ به صورت عددی")]
        public string CaptchaInputText { get; set; }
    }
    public class ContactUsShowViewModel
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Explain { get; set; }
        public DateTime Date { get; set; }
        public string FLName { get; set; }
        public bool StatusRead { get; set; }
        public bool StatusAnswer { get; set; }
    }
}
