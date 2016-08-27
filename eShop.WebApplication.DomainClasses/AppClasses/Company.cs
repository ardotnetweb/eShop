
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class Company
    {
        [Required(ErrorMessage = "یکی از کمپانی های زیر را انتخاب نمایید")]
        public virtual int Id { get; set; }

        [Display(Name = "نام کمپانی سازنده")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "نام کمپانی حدئقل باید 3 کاراکتر و حداکثر 50 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا نام کمپانی را وارد نمایید")]
        public virtual string Name { get; set; }



        [Display(Name = "تاریخچه کمپانی سازنده")]
        [StringLength(maximumLength: 10000, MinimumLength = 0,
            ErrorMessage = "توضیحات مربوط به کمپانی حدئقل باید 0 کاراکتر و حداکثر 2000 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفاتوضیحات مربوط به کمپانی را وارد نمایید")]
        public virtual string Explain { get; set; }



        [Display(Name = "تارنمای کمپانی سازنده")]
        [DataType(DataType.Url)]
        [StringLength(maximumLength: 50, MinimumLength = 8, ErrorMessage = "نام کمپانی حدئقل باید 8 کاراکتر و حداکثر 50 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا آدرس تارنمای کمپانی را وارد نمایید")]
        public virtual string Address { get; set; }



        [Display(Name = "برند کمپانی سازنده")]
        public virtual string ImageLogo { get; set; }


        [Display(Name = "عنوان کمپانی سازنده")]
        public virtual string Title { get; set; }

        public virtual List<Product> Products { get; set; }


    }
}
