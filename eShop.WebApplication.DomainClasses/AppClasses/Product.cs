
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class Product : IEquatable<Product>
    {
        public Product()
        {
            ImageProducts = new List<ImageProduct>();
            Comments = new List<Comment>();
            Packages = new List<Package>();
            Labels = new List<Label>();
            Interests = new List<Interest>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public string PrimryImage { get; set; }
        public virtual Category Category { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<ImageProduct> ImageProducts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public int VisitNumber { get; set; }
        public virtual string Explain { get; set; }
        public virtual List<Package> Packages { get; set; }
        public double Price { get; set; }

        public virtual List<Label> Labels { get; set; }
        public virtual List<Interest> Interests { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual byte Recomend { get; set; }
        [NotMapped]
        public int NumberRecomend
        {
            get { return Convert.ToInt32(this.Recomend); }
        }


        public bool Equals(Product other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Name.Equals(other.Name);
        }
        public override int GetHashCode()
        {
            int hashProductName = Name == null ? 0 : Name.GetHashCode();
            return hashProductName;
        }
    }
}
