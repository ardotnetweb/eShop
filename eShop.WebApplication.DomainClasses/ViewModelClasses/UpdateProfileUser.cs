using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class UpdateProfileUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام را وارد نمایید")]
        public string Name { get; set; }




        [Required(ErrorMessage = "نام خانوادگی را وارد نمایید")]
        public string Family { get; set; }


        [Required(ErrorMessage = "تمایل به دریافت پیام")]
        public bool ReciveMessage { get; set; }



        [Required(ErrorMessage = "استان خود را انتخاب نمایید")]
        public int Province_Id { get; set; }

        public string Province { get; set; }



        [Required(ErrorMessage = "شهرستان خود را انتخاب نمایید")]
        public int City_Id { get; set; }

        public string City { get; set; }


        [Required(ErrorMessage = "کد پستی خود را وارد نمایید")]
        public string PostalCode { get; set; }



        [Required(ErrorMessage = "آدرس خود را وارد نمایید")]
        public string Address { get; set; }


        [Required(ErrorMessage = "شماره همراه خود را وارد نمایید")]
        public string Phone { get; set; }

        public string Email { get; set; }
        public bool StatusDisableUser { get; set; }
        public DateTime DateDisableUser { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
