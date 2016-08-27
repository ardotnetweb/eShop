
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Explain { get; set; }
        public DateTime DateTimeNews { get; set; }
        public string Image { get; set; }
    }
}
