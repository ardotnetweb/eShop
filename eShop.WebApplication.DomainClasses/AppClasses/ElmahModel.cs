using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class ElmahModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string IP { get; set; }
        public string Message { get; set; }
        public string HttpMethod { get; set; }
    }
}
