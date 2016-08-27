using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ResultUltimateChartViewModel
    {
        public string[] ListName { get; set; }
        public object[] ListValue { get; set; }
        public string SumSales { get; set; }
        public string MaxSales { get; set; }
        public string MinSales { get; set; }
    }
}
