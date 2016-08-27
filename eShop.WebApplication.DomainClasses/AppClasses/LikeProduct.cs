
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class LikeProduct
    {
        public int Id { get; set; }

        public int Id_Product { get; set; }
        public int LikeNumber { get; set; }
        public virtual List<IPLikeProduct> IPLikeProducts { get; set; } 
    }
}
