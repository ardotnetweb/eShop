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

namespace WebApplication.BaseClassWebApplication
{
    public static class InitializationStructuremap
    {
        public static void Init()
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion
                          <DataContext, Configuration>());

            ObjectFactory.Initialize(x =>
            {
                x.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<DataContext>();
                x.For<DataContext>().HybridHttpOrThreadLocalScoped().Use(context => (DataContext)context.GetInstance<IUnitOfWork>());
                x.For<DbContext>().HybridHttpOrThreadLocalScoped().Use(context => (DataContext)context.GetInstance<IUnitOfWork>());
                x.For<IUserStore<ApplicationUser, int>>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<UserStore<ApplicationUser, CustomRole, int, CustomUserLogin,
                   CustomUserRole, CustomUserClaim>>();
                x.For<IRoleStore<CustomRole, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<RoleStore<CustomRole, int, CustomUserRole>>();

                x.For<IAuthenticationManager>()
                      .Use(() => HttpContext.Current.GetOwinContext().Authentication);

                x.For<IApplicationSignInManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationSignInManager>();

                x.For<IApplicationRoleManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationRoleManager>();

                x.For<IIdentityMessageService>().Use<SmsService>();
                x.For<IIdentityMessageService>().Use<EmailService>();


                x.For<IApplicationUserManager>().HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationUserManager>()
                   .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
                   .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
                   .Setter<IIdentityMessageService>(userManager => userManager.SmsService).Is<SmsService>()
                   .Setter<IIdentityMessageService>(userManager => userManager.EmailService).Is<EmailService>();


                x.For<ApplicationUserManager>().HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());

                x.For<ICustomRoleStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomRoleStore>();

                x.For<ICustomUserStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomUserStore>();






                x.For<ICategoryService>().Use<CategoryService>();
                x.For<ICommentService>().Use<CommentService>();
                x.For<IDiscountService>().Use<DiscountService>();
                x.For<IEbookService>().Use<EbookService>();
                x.For<IFavoriteUserService>().Use<FavoriteUserService>();
                x.For<ILabelService>().Use<LabelService>();
                x.For<INewsService>().Use<NewsService>();
                x.For<IProductService>().Use<ProductService>();
                x.For<ICompanyService>().Use<CompanyService>();
                x.For<IImageProductService>().Use<ImageProductService>();
                x.For<IPackageService>().Use<PackageService>();
                x.For<ISliderService>().Use<SliderService>();
                x.For<IProvinceService>().Use<ProvinceService>();
                x.For<ICityService>().Use<CityService>();
                x.For<IElmahService>().Use<ElmahService>();
                x.For<IBlockService>().Use<BlockService>();
                x.For<ISaleService>().Use<SaleService>();
                x.For<IOrderService>().Use<OrderService>();
                x.For<IContactService>().Use<ContactService>();

                //x.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use(() =>
                //new DataContext());

            });

            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(
                new ControllerFactoryStructuremap());
        }

    }
}