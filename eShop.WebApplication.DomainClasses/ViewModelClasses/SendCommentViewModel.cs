using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class SendCommentViewModel
    {
        [Required(ErrorMessage = "لطفا نظر را وارد نمایید")]
        [StringLength(maximumLength: 2500, MinimumLength = 25, ErrorMessage = "نظر شما حدئقل باید 25 کاراکتر و حداکثر 2500 کاراکتر باشد")]
        public string Explain { get; set; }
        public int Product_Id { get; set; }
    }
}
