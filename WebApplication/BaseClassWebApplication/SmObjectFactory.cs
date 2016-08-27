using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Web;
using eShop.WebApplication.DataLayer.EntityContext;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using eShop.WebApplication.ServiceLayer.AppServices.ImplamentService;
using System.Web.Mvc;
using System.Data.Entity;
using eShop.WebApplication.DomainClasses.AppClasses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Implament;
using eShop.WebApplication.DataLayer.Migrations;
using System.Threading;

namespace WebApplication.BaseClassWebApplication
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {
                 ioc.For<IUnitOfWork>() 
                    .HybridHttpOrThreadLocalScoped() 
                    .Use<DataContext>() 
                    // Remove these 2 lines if you want to use a connection string named connectionString1, defined in the web.config file. 
                    .Ctor<string>("DefaultConnection")
                    .Is("Data Source=(local);Initial Catalog=eShopWebApplication;Integrated Security = true");


                 ioc.For<DataContext>().HybridHttpOrThreadLocalScoped()
                    .Use(context => (DataContext)context.GetInstance<IUnitOfWork>()); 
                 ioc.For<DbContext>().HybridHttpOrThreadLocalScoped()
                    .Use(context => (DataContext)context.GetInstance<IUnitOfWork>()); 
 
 
                 ioc.For<IUserStore<ApplicationUser, int>>() 
                     .HybridHttpOrThreadLocalScoped() 
                     .Use<UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>>(); 
 
 
                 ioc.For<IRoleStore<CustomRole, int>>() 
                     .HybridHttpOrThreadLocalScoped() 
                     .Use<RoleStore<CustomRole, int, CustomUserRole>>(); 
 
 
                 ioc.For<IAuthenticationManager>() 
                       .Use(() => HttpContext.Current.GetOwinContext().Authentication); 
 
 
                 ioc.For<IApplicationSignInManager>() 
                       .HybridHttpOrThreadLocalScoped() 
                       .Use<ApplicationSignInManager>(); 
 
 
                 ioc.For<IApplicationRoleManager>() 
                       .HybridHttpOrThreadLocalScoped() 
                       .Use<ApplicationRoleManager>(); 
 
 
                 // map same interface to different concrete classes 
                 ioc.For<IIdentityMessageService>().Use<SmsService>(); 
                 ioc.For<IIdentityMessageService>().Use<EmailService>(); 
 
 
                 ioc.For<IApplicationUserManager>().HybridHttpOrThreadLocalScoped() 
                    .Use<ApplicationUserManager>() 
                    .Ctor<IIdentityMessageService>("smsService").Is<SmsService>() 
                    .Ctor<IIdentityMessageService>("emailService").Is<EmailService>() 
                    .Setter<IIdentityMessageService>(userManager => userManager.SmsService).Is<SmsService>() 
                    .Setter<IIdentityMessageService>(userManager => userManager.EmailService).Is<EmailService>(); 
 
 
                 ioc.For<ApplicationUserManager>().HybridHttpOrThreadLocalScoped() 
                   .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>()); 
 
 
                ioc.For<ICustomRoleStore>() 
                       .HybridHttpOrThreadLocalScoped() 
                       .Use<CustomRoleStore>(); 
 
 
                 ioc.For<ICustomUserStore>() 
                       .HybridHttpOrThreadLocalScoped() 
                       .Use<CustomUserStore>(); 


                //config.For<IDataProtectionProvider>().Use(()=> app.GetDataProtectionProvider()); // In Startup class

                //ioc.For<ICategoryService>().Use<EfCategoryService>();
                //ioc.For<IProductService>().Use<EfProductService>();



                ioc.For<ICategoryService>().Use<CategoryService>();
                ioc.For<ICommentService>().Use<CommentService>();
                ioc.For<IDiscountService>().Use<DiscountService>();
                ioc.For<IEbookService>().Use<EbookService>();
                ioc.For<IFavoriteUserService>().Use<FavoriteUserService>();
                ioc.For<ILabelService>().Use<LabelService>();
                ioc.For<INewsService>().Use<NewsService>();
                ioc.For<IProductService>().Use<ProductService>();
                ioc.For<ICompanyService>().Use<CompanyService>();
                ioc.For<IImageProductService>().Use<ImageProductService>();
                ioc.For<IPackageService>().Use<PackageService>();
                ioc.For<ISliderService>().Use<SliderService>();
                ioc.For<IProvinceService>().Use<ProvinceService>();
                ioc.For<ICityService>().Use<CityService>();
                ioc.For<IElmahService>().Use<ElmahService>();
                ioc.For<IBlockService>().Use<BlockService>();
                ioc.For<ISaleService>().Use<SaleService>();
                ioc.For<IOrderService>().Use<OrderService>();
                ioc.For<IContactService>().Use<ContactService>();


            });
        }
    }
}