
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class IPLikeProduct
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }
        public LikeProduct LikeProduct { get; set; }
    }
}
