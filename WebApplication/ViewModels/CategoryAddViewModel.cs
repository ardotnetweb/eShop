﻿using eShop.WebApplication.DomainClasses.AppClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.ViewModels
{
    public class CategoryAddViewModel
    {

        [Remote("CheckName", "Category", ErrorMessage = "این نام قبلا در پایگاه داده ثبت شده است")]
        [Required(ErrorMessage = "نام گروه را وارد نمایید")]
        [Display(Name = "نام گروه")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "حدئقل 3 کاراکتر و حداکثر 20 کاراکتر")]
        public  string Name { get; set; }

        //Refrenced To Parent, No Refrenced To Id Model
        [Display(Name = "ریشه گروه")]
        public int Parent_Id { get; set; }
    }
}