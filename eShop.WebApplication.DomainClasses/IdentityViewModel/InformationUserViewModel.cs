using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.IdentityViewModel
{
    public class InformationUserViewModel
    {
        public int Id { get; set; }


        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "حدئقل تعداد کاراکتر های یک نام 3 و حداکثر 20 می باشد", MinimumLength = 3)]
        [Required(ErrorMessage="نام خود را وارد نمایید")]
        [Display(Name="نام")]
        public string Name { get; set; }


        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "حدئقل تعداد کاراکتر های یک نام 3 و حداکثر 20 می باشد", MinimumLength =2)]
        [Required(ErrorMessage = "نام خود را وارد نمایید")]
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }


        [Required(ErrorMessage = "وضعیت خود را نسبت به دریافت خبر نامه مشخص نمایید")]
        [Display(Name = "دریافت خبر نامه")]
        public bool ReciveMessage { get; set; }
    }
}




        //[DataType(DataType.PhoneNumber)]
        //[StringLength(11, ErrorMessage = "تعداد شماره ها 11 رقم باید باشد", MinimumLength = 11)]
        //[Required(ErrorMessage = "شماره موبایل خود را وارد نمایید")]
        //[Display(Name = "شماره موبایل")]
        //[Phone(ErrorMessage="شماره موبایل را به درستی وارد نمایید")]
        //public string CellPhone { get; set; }

        //[DataType(DataType.PhoneNumber)]
        //[StringLength(11, ErrorMessage = "تعداد شماره ها 11 رقم باید باشد", MinimumLength = 11)]
        //[Required(ErrorMessage = "شماره منزل یا دفتر خود را وارد نمایید")]
        //[Display(Name = "شماره منزل یا دفتر")]
        //[Phone(ErrorMessage = "شماره منزل یا دفتر را به درستی وارد نمایید")]
        //public string Phone { get; set; }