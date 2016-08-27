using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double Postage { get; set; }
        public string StatusTypePay { get; set; }
        public int User_Id { get; set; }
        public bool StatusSend { get; set; }
        public string TrackingNumber { get; set; }
        public string FLName { get; set; }
        public bool StatusUltimate { get; set; }
    }
}
