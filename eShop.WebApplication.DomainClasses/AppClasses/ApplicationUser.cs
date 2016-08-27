using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class ApplicationUser :
        IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        //For Disbale User from Syste

        public DateTime DateDisableUser { get; set; }
        public bool DisableUser { get; set; }


        public string Name { get; set; }
        public string Family { get; set; }
        public bool ReciveMessage { get; set; }
        public virtual AddressUser Address { get; set; }
        public virtual List<Interest> Interests { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<ContactUs> Contacts { get; set; }
        public virtual List<Sale> Sales { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
