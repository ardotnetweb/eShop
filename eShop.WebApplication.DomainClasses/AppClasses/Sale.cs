using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double Postage { get; set; }

        //True Equals Active ! False Equal No Active
        public bool StatusUltimate { get; set; }
        
        //True . The Package Sended
        public bool StatusSend { get; set; }
        [ForeignKey("User_Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int? User_Id { get; set; }
        public string StatusTypePay { get; set; }
        public virtual  List<Order> Orders{ get; set; }

        public string TrackingNumber { get; set; }
    }
}
