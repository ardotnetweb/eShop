using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ShowSliderViewModel
    {
        [Key]
        public int Id { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "تصویر اسلایدر")]
        [Required(ErrorMessage = "یک تصویر وارد نمایید")]
        public string ImageSlider { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "موضوع تصویر")]
        [Required(ErrorMessage = "موضوع تصویر آموزشی را وارد نمایید")]
        public string TitleSlider { get; set; }


        [DataType(DataType.Url)]
        [Display(Name = "لینک محتوای تصویر آموزشی")]
        [Required(ErrorMessage = "لینک محتوای تصویر آموزشی را وارد نمایید")]
        public string AddressSlider { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "توضیحات تصویر")]
        public string ExplainSlider { get; set; }
    }

    public class DeleteSliderViewModel
    {
        public int Id { get; set; }
        public string ImageSlider { get; set; }
        public string Title { get; set; }
    }
}
