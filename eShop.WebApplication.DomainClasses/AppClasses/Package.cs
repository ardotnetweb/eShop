using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class Package
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام پکیج آموزشی را وارد نمایید")]
        [Display(Name = "نام پکیج آموزشی")]
        public string Name { get; set; }

        [Display(Name = "تصویر پکیج آموزشی")]
        public string Image { get; set; }

        [Required(ErrorMessage = "درصد تخفیف را وارد نمایید")]
        [Display(Name = "درصد تخفیف")]
        public int Percent { get; set; }
        [Required(ErrorMessage = "قیمت خالص پکیج باید محاسبه شود. لطفا بر روی گزینه محاسبه درصد تخفیفات کلیک نمایید")]
        [Display(Name = "قیمت واقعی پکیج")]
        public double OriginalPrice { get; set; }

        [Required(ErrorMessage = "قیمت نهایی پکیج با احتساب تخفبف  باید محاسبه شود. لطفا بر روی گزینه محاسبه درصد تخفیفات کلیک نمایید")]
        [Display(Name = "قیمت پکیج با احتساب تخفبف")]
        public double DisCountPrice { get; set; }

        public virtual List<Product> Products { get; set; }


        [UIHint("PersianDatePicker")]
        [Required(ErrorMessage = "تاریخ شروع پکیج را وارد نمایید")]
        [Display(Name = "تاریخ شروع پکیج")]
        public DateTime StartDate { get; set; }


        [UIHint("PersianDatePicker")]
        [Required(ErrorMessage = "تاریخ پایان پکیج را وارد نمایید")]
        [Display(Name = "تاریخ پایان پکیج")]
        public DateTime EndDate { get; set; }

        [Display(Name = "وضعیت نمایش پکیج")]
        public bool IsShow { get; set; }

        [AllowHtml]
        [Display(Name = "توضحیات مربوط به پکیج")]
        public string Explain { get; set; }


        public string TimeEducation { get; set; }


        public virtual Category Category { get; set; }



    }
}
