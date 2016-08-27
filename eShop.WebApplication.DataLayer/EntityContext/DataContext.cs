using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eShop.WebApplication.DataLayer.EntityContext
{
    public class DataContext : IdentityDbContext<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>, IUnitOfWork
    {
        
        public DataContext() : base("DefaultConnection") 
        {
            Database.CommandTimeout = 180;   
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new ProductConfig());


            modelBuilder.Entity<BlockModel>().ToTable("Blocks");
            modelBuilder.Entity<ElmahModel>().ToTable("Elmahs");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<CustomRole>().ToTable("Roles");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");




        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Discount> DisCounts { get; set; }
        public DbSet<eBook> eBooks { get; set; }
        public DbSet<FavoriteUser> FavoriteUsers { get; set; }
        public DbSet<ImageProduct> ImageProducts { get; set; }
        public DbSet<LikeProduct> LikeProducts { get; set; }
        public DbSet<IPLikeProduct> IpLikeProducts { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<News> NewsWeb { get; set; }
        public DbSet<VisiteBook> VisiteBooks { get; set; }
        public DbSet<VisitProduct> VisitProducts { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<AddressUser> Addresses { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<ElmahModel> Elmahs { get; set; }
        public DbSet<BlockModel> Blocks { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<Sale> Sales { get; set; }

        //Implamint UnitOfWork Pattern Design

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            base.Set<TEntity>().AddRange(entities);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var item in entities)
                base.Set<TEntity>().Attach(item);

            base.Set<TEntity>().RemoveRange(entities);
        }
        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
            this.Database.Initialize(force: true);
        }
    }
}
