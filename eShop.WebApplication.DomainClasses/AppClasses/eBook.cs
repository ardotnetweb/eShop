
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class eBook 
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime DateTimeeBook { get; set; }
        public DateTime DateTimePublish { get; set; }
        public string Writer { get; set; }
        public int CountPage { get; set; }
        public string Language { get; set; }
        public string Explain { get; set; }
        //public List<Label> Labels { get; set; }
        public int CountVisit { get; set; }
    }
}
