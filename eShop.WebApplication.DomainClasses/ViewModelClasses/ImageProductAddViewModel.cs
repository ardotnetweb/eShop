using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ImageProductAddViewModel
    {
        [FileTypes("jpg,png,jpeg")]
        [FileSize(51200)] //50 KB * 1024
        public virtual HttpPostedFileBase Image1 { get; set; }

        [FileTypes("jpg,png,jpeg")]
        [FileSize(51200)] //50 KB * 1024
        public virtual HttpPostedFileBase Image2 { get; set; }


        [FileTypes("jpg,png,jpeg")]
        [FileSize(51200)] //50 KB * 1024
        public virtual HttpPostedFileBase Image3 { get; set; }


        [FileTypes("jpg,png,jpeg")]
        [FileSize(51200)] //50 KB * 1024
        public virtual HttpPostedFileBase Image4 { get; set; }


        [FileTypes("jpg,png,jpeg")]
        [FileSize(51200)] //50 KB * 1024
        public virtual HttpPostedFileBase Image5 { get; set; }


        [FileTypes("jpg,png,jpeg")]
        [FileSize(51200)] //50 KB * 1024
        public virtual HttpPostedFileBase Image6 { get; set; }


    }
}
