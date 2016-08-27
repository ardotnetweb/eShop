
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class ImageProduct
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public Product Product { get; set; }
    }
}
