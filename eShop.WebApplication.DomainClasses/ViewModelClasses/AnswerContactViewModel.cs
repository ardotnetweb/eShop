using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class AnswerContactViewModel
    {
        public int Contact_Id { get; set; }
        public int User_Id { get; set; }
        public string FLName { get; set; }

        [StringLength(500,ErrorMessage="حداکثر تعداد کاراکتر 100 و حدئقل تعداد کاراکتر 10 می باشد",MinimumLength=10)]
        [Required(ErrorMessage="متن پیام را وارد نمایید")]
        public string Message { get; set; }
    }
}
