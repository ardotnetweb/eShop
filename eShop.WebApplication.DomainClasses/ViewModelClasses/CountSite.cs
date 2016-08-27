using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class CountSite
    {
        public int ContSaleNotConfirmed { get; set; }
        public int CountCommentNotRead { get; set; }
        public int CountUsers { get; set; }
        public int CountMessageNotRead { get; set; }
        public int CountLabels { get; set; }
        public int CountOfferProduct { get; set; }
        public int CountActivePackage { get; set; }
        public int CountDisablePackage { get; set; }
    }
    public class CountInfoUser
    {
        public int CountOrder { get; set; }
        public int CountNotConfirmed { get; set; }
        public int CountProcess { get; set; }
        public int CountSend { get; set; }
        public int CountComment { get; set; }
        public int CountMessage { get; set; }
        public int CountFavorite { get; set; }
    }
}
