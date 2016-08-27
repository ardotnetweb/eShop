using eShop.WebApplication.DomainClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BaseClassWebApplication;

namespace WebApplication.ViewModels
{
    public class CompanyAddViewModel
    {

        public int Id { get; set; }

        [Remote("CheckName", "Company", ErrorMessage = "این نام قبلا در پایگاه داده ثبت شده است")]
        [Display(Name = "نام کمپانی سازنده")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "نام کمپانی حدئقل باید 3 کاراکتر و حداکثر 50 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا نام کمپانی را وارد نمایید")]
        public virtual string Name { get; set; }



        [AllowHtml]
        [Display(Name = "تاریخچه کمپانی سازنده")]
        [Required(ErrorMessage = "لطفاتوضیحات مربوط به کمپانی را وارد نمایید")]
        public virtual string Explain { get; set; }


        [Remote("CheckAddress", "Company", ErrorMessage = "این آدرس قبلا در پایگاه داده ثبت شده است")]
        [Display(Name = "تارنمای کمپانی سازنده")]
        [DataType(DataType.Url)]
        [StringLength(maximumLength: 50, MinimumLength = 8, ErrorMessage = "نام کمپانی حدئقل باید 8 کاراکتر و حداکثر 50 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا آدرس تارنمای کمپانی را وارد نمایید")]
        public virtual string Address { get; set; }


        [FileTypes("jpg,png,jpeg")]
        [FileSize(20480)]
        [Display(Name = "برند کمپانی سازنده")]
        public virtual HttpPostedFileBase Image { get; set; }

        public string ImageLogo { get; set; }

        [Display(Name = "عنوان کمپانی سازنده")]
        public virtual string Title { get; set; }

    }
}