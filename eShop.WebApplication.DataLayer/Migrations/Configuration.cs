namespace eShop.WebApplication.DataLayer.Migrations
{
    using eShop.WebApplication.DataLayer.EntityContext;
    using eShop.WebApplication.DomainClasses.AppClasses;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DataContext context)
        {
            context.Categories.AddOrUpdate(x => new { x.Name },
                new Category { Name = "برنامه نویسی وب" },
                new Category { Name = "طراحی وب" },
                new Category { Name = "پایگاه داده" },
                new Category { Name = "تکنولوژی های دیگر" },
                new Category { Name = "برنامه نویسی موبایل" });
        }
    }
}
