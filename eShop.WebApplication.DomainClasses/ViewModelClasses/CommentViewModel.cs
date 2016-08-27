using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Explain { get; set; }
        public DateTime DateTime { get; set; }
        public bool DisplayStatus { get; set; }
        public bool ReadStatus { get; set; }
        public int Product_Id { get; set; }
        public string ProductName { get; set; }
        public int User_Id { get; set; }
        public string UserName { get; set; }
    }

    public class CommentViewModel_
    {
        public int Id { get; set; }
        public string Explain { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public int product_Id { get; set; }
        public string product_Name { get; set; }
    }
}
