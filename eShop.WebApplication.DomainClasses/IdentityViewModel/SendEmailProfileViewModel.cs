using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.IdentityViewModel
{
    public class SendEmailProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public string Explain { get; set; }
        public string Subject { get; set; }
    }
    public class SendSMSProfileViewModel
    {
        public int Id { get; set; }
        public string FLName { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "متن پیام را  وارد نمایید")]
        [StringLength(500, ErrorMessage = "متن پیام شما باید حدئقل 10 کاراکتر و حداکثر 500 کاراکتر باشد", MinimumLength = 10)]
        public string Explain { get; set; }
       
        public string Phone { get; set; }
    }
}
