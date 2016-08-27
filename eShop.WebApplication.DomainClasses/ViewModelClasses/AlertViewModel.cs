using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class AlertViewModel
    {
        public string Alert { get; set; }
        public AlertMode Status { get; set; }
    }
}
