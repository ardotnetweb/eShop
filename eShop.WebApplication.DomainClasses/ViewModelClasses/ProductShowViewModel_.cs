using eShop.WebApplication.DomainClasses.AppClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class ProductShowViewModel_
    {
        public ProductShowViewModel_()
        {
            Labels = new List<Label>();
            ImageProducts = new List<ImageProduct>();
            Comments = new List<CommentViewModel_>();
        }
        public int Id { get; set; }
        public CategoryDropListViewModel_ Category { get; set; }
        public CompanyDropListViewModel Company { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public virtual List<Label> Labels { get; set; }
        public string Name { get; set; }
        public string PrimaryImage { get; set; }
        public virtual List<ImageProduct> ImageProducts { get; set; }
        public string Explain { get; set; }
        public double Price { get; set; }
        public int CountVisit { get; set; }
        public  byte Recomend { get; set; }
        public virtual List<CommentViewModel_> Comments { get; set; }
    }
}
