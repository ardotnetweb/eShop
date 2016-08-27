using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class CompleteInformation
    {
        [Required(ErrorMessage = "لطفا نام خود را وارد نمایید")]
        [MaxLength(20, ErrorMessage = "حداکثر تعداد کاراکتر برای نام 20 است")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد نمایید")]
        [MaxLength(20, ErrorMessage = "حداکثر تعداد کاراکتر برای نام خانوادگی 20 است")]
        public string Family { get; set; }

        [Required(ErrorMessage = "لطفا شماره موبایل خود را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر برای شماره موبایل 11 است")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "لطفا استان خود را انتخاب نمایید")]
        public int Province_Id { get; set; }


        [Required(ErrorMessage = "لطفا شهرستان خود را انتخاب نمایید")]
        public int City_Id { get; set; }

        [Required(ErrorMessage = "لطفا آدرس محل سکونت خود را وارد نمائید")]
        [MaxLength(200, ErrorMessage = "حداکثر تعداد کاراکتر برای شماره موبایل 200 است")]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا کد پستی محل سکونت خود را وارد نمائید")]
        [MaxLength(200, ErrorMessage = "حداکثر تعداد کاراکتر برای  کد پستی 10 است")]
        public string PostalCode { get; set; }
    }
}
