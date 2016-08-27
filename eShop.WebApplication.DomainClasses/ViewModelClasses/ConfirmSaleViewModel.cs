using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ConfirmSaleViewModel
    {
        public int Usder_Id { get; set; }
        public int Sale_Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage="کد رهگیری را وارد نمایید")]
        public string TrackingNumber { get; set; }
    }
}
