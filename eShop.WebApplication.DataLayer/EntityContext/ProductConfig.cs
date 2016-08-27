using eShop.WebApplication.DomainClasses.AppClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DataLayer.EntityContext
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            this.HasMany(x => x.Packages)
                .WithMany(x => x.Products)
                .Map(relation =>
                {
                    relation.ToTable("ProductToPackage");
                    //relation.MapLeftKey("Package_Id");
                    //relation.MapRightKey("Product_Id");
                    relation.MapRightKey("Package_Id");
                    relation.MapLeftKey("Product_Id");
                });


            this.HasMany(x => x.Labels)
                .WithMany(x => x.Products)
                .Map(relation =>
                {
                    relation.ToTable("LabelToProduct");
                    relation.MapRightKey("Label_Id");
                    relation.MapLeftKey("Product_Id");
                });
        }
    }
}
