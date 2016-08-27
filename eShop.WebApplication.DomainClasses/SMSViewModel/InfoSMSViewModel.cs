using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.SMSViewModel
{
    public class InfoSMSViewModel
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public DateTime DateTime { get; set; }
        public string Body { get; set; }
    }
}
