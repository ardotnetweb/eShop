using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.ViewModels
{
    public class CityAddViewModel
    {
        [Required(ErrorMessage = "یکی از استان ها را انتخاب نمایید")]       
        public int ProvinceId { get; set; }

        [Remote("CheckName", "City", ErrorMessage = "این نام قبلا در پایگاه داده ثبت شده است")]

        [Required(ErrorMessage = "نام را وارد نمایید")]
        public string Name { get; set; }
    }
}