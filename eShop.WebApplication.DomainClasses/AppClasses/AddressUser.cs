using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class AddressUser
    {
        [Key, ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        public string PostalCode { get; set; }
        public virtual Province Province { get; set; }
        public virtual City City { get; set; }
        public string Address { get; set; }
        public virtual ApplicationUser ApplicationUser { set; get; }
    }
}
