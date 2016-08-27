using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.ViewModels
{
    public class PackageAddViewModel
    {


        [Remote("CheckName", "Package", ErrorMessage = "این نام قبلا در پایگاه داده ثبت شده است")]
        [Required(ErrorMessage = "نام پکیج آموزشی را وارد نمایید")]
        [Display(Name = "نام پکیج آموزشی")]
        public string Name { get; set; }


        [Required(ErrorMessage = "درصد تخفیف را وارد نمایید")]
        [Display(Name = "درصد تخفیف")]
        public int Percent { get; set; }


        [Required(ErrorMessage = "قیمت خالص پکیج باید محاسبه شود. لطفا بر روی گزینه محاسبه درصد تخفیفات کلیک نمایید")]
        [Display(Name = "قیمت واقعی پکیج")]
        public double OriginalPrice { get; set; }

        [Required(ErrorMessage = "قیمت نهایی پکیج با احتساب تخفبف  باید محاسبه شود. لطفا بر روی گزینه محاسبه درصد تخفیفات کلیک نمایید")]
        [Display(Name = "قیمت پکیج با احتساب تخفبف")]
        public double DisCountPrice { get; set; }



        [UIHint("PersianDatePicker")]
        [Required(ErrorMessage = "تاریخ شروع پکیج را وارد نمایید")]
        [Display(Name = "تاریخ شروع پکیج")]
        public DateTime StartDate { get; set; }


        [UIHint("PersianDatePicker")]
        [Required(ErrorMessage = "تاریخ پایان پکیج را وارد نمایید")]
        [Display(Name = "تاریخ پایان پکیج")]
        public DateTime EndDate { get; set; }

        [AllowHtml]
        [Display(Name = "توضحیات مربوط به پکیج")]
        public string Explain { get; set; }
    }
}