using eShop.WebApplication.DomainClasses.AppClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DataLayer.EntityContext
{
    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            this.HasOptional(x => x.Parent)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
