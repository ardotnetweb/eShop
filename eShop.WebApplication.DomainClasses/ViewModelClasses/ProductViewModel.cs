using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;
using System.Web.Mvc;
using System.Web;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ProductViewModel
    {
        [Display(Name = "دسته گروه آموزشی محصول")]
        [Required(ErrorMessage = "نام کمپانی سازنده این محصول آموزشی را وارد نمایید")]
        public int Category_Id { get; set; }


        [Required(ErrorMessage = "یکی از گروه های آموزشی مربوط به این محصول را انتخاب نمایید")]
        [Display(Name = "کمپانی سازنده محصول")]
        public int Company_Id { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "تاریخ درج محصول در سیستم را وارد نمایید")]
        [Display(Name = "تاریخ درج محصول در سیستم")]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "مدت زمان آموزش را وارد نمایید")]
        [Display(Name = "مدت زمان آموزش")]
        public TimeSpan Time { get; set; }


        [Required(ErrorMessage = "لطفا برای این محصول یک یا چند برچسب وارد نمایید")]
        [Display(Name = "برچسب های محصول")]
        public int [] Labels { get; set; }


        [StringLength(maximumLength: 150, MinimumLength = 10, ErrorMessage = "نام محصول باید بین 10 تا 150 کاراکتر باشد")]
        [Required(ErrorMessage = "نام محصول را وارد نمایید")]
        [Display(Name = "نام محصول")]
        //[Remote("IsNameCheck", "product", "admin", ErrorMessage = "لفا نام دیگری را وارد نمایید این نام قبلا در پایگاه داده ثبت شده است")]
        public string Name { get; set; }


        [FileTypes("jpg,png,jpeg")]
        [FileSize(200)] //50 KB * 1024
        [Required(ErrorMessage = "تصویر اصلی محصول را وارد نمایید")]
        [Display(Name = "تصویر اصلی محصول")]
        public virtual HttpPostedFileBase Image { get; set; }

        public string PrimaryImage { get; set; }


        [AllowHtml]
        [Required(ErrorMessage = " توضحات مربوط به محصول را وارد نمایید")]
        [Display(Name = "توضحات مربوط به محصول")]
        public string Explain { get; set; }

        [Required(ErrorMessage = " قیمت محصول را وارد نمایید")]
        [Display(Name = "قیمت محصول")]
        public double Price { get; set; }
    }




    public class ProductShowViewModel
    {
        public ProductShowViewModel()
        {
            Labels = new List<Label>();
        }
        public int Id { get; set; }
        [Display(Name = "گروه آموزشی ")]
        public string Category_Name { get; set; }


        [Display(Name = "کمپانی سازنده")]
        public string Company_Name { get; set; }


        [Display(Name = "تاریخ درج محصول")]
        public DateTime Date { get; set; }


        [Display(Name = "مدت زمان آموزش")]
        public TimeSpan Time { get; set; }

        //On Show Details dosen't Use it
        [Display(Name = "برچسب های محصول")]
        public int [] LabelsId { get; set; }


        //For Show Labels In a String
        [Display(Name = "برچسب های محصول")]
        public string LabelsName { get; set; }


        public virtual List<Label> Labels { get; set; }

        [Required(ErrorMessage = "نام محصول ")]
        [Display(Name = "نام محصول")]
        public string Name { get; set; }


        [Display(Name = "تصویر اصلی محصول")]
        public string PrimaryImage { get; set; }


        [Display(Name = "تصاویر بیشتر محصول")]
        public List<ImageProduct> ImageProducts { get; set; }


        [Display(Name = "توضحات مربوط به محصول")]
        public string Explain { get; set; }

        public double Price { get; set; }

        public int CountVisit { get; set; }
        public  byte Recomend { get; set; }

    }


    public class ProductEditViewModel
    {
        public ProductEditViewModel()
        {
            Labels = new List<Label>();
            ImageProducts = new List<ImageProduct>();
        }
        public virtual int Id { get; set; }

        [Display(Name = "دسته گروه آموزشی محصول")]
        [Required(ErrorMessage = "نام کمپانی سازنده این محصول آموزشی را وارد نمایید")]
        public int Category_Id { get; set; }


        [Required(ErrorMessage = "یکی از گروه های آموزشی مربوط به این محصول را انتخاب نمایید")]
        [Display(Name = "کمپانی سازنده محصول")]
        public int Company_Id { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "تاریخ درج محصول در سیستم را وارد نمایید")]
        [Display(Name = "تاریخ درج محصول در سیستم")]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "مدت زمان آموزش را وارد نمایید")]
        [Display(Name = "مدت زمان آموزش")]
        public TimeSpan Time { get; set; }


        [Required(ErrorMessage = "لطفا برای این محصول یک یا چند برچسب وارد نمایید")]
        [Display(Name = "برچسب های محصول")]
        public int [] LabelsId { get; set; }

        public virtual List<Label> Labels { get; set; }


        [StringLength(maximumLength: 150, MinimumLength = 10, ErrorMessage = "نام محصول باید بین 10 تا 150 کاراکتر باشد")]
        [Required(ErrorMessage = "نام محصول را وارد نمایید")]
        [Display(Name = "نام محصول")]
        public string Name { get; set; }


        [FileTypes("jpg,png,jpeg")]
        [FileSize(200)] //50 KB * 1024
        [Display(Name = "تصویر اصلی محصول")]
        public virtual HttpPostedFileBase Image { get; set; }

        public string PrimaryImage { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = " توضحات مربوط به محصول را وارد نمایید")]
        [Display(Name = "توضحات مربوط به محصول")]
        public string Explain { get; set; }

        public virtual List<ImageProduct> ImageProducts { get; set; }

        [Required(ErrorMessage = " قیمته محصول را وارد نمایید")]
        [Display(Name = "قیمت محصول")]
        public double Price { get; set; }

    }


    public class ProductShow_Create_Package_ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company_Name { get; set; }
        public TimeSpan Time { get; set; }
        public double Price { get; set; }
        public int Category_Id { get; set; }
    }

    public class IntersetUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company_Name { get; set; }
        public int Company_Id { get; set; }
        public double Price { get; set; }
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
    }



    public class ProductPackageViewModel
    {
        public int Id { get; set; }
        public string Category_Name { get; set; }
        //public string Company_Name { get; set; }
        public double PricePackage { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Name { get; set; }
        public string PrimaryImage { get; set; }

    }
    


    public class ProductShowProfileViewModel
    {
        public int Id { get; set; }
        public int Company_Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Category_Name { get; set; }
        public string Company_Name { get; set; }
    }

    public class EditProductForOffer
    {
        public int Id { get; set; }
        public byte Recomend { get; set; }
    }

}
