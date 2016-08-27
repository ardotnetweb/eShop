using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Web;
using System.Web;
using Microsoft.Owin.Security.Cookies;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using WebApplication.BaseClassWebApplication;


[assembly: OwinStartupAttribute(typeof(WebApplication.StartUp))]
namespace WebApplication
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
        private static void ConfigureAuth(IAppBuilder app)
        {

            //ObjectFactory.Container.Configure(x => x.For<IDataProtectionProvider>()
            //    .HybridHttpOrThreadLocalScoped()
            //    .Use(() => app.GetDataProtectionProvider()));

            //ObjectFactory.Container.GetInstance<IApplicationUserManager>().SeedDatabase();
            SmObjectFactory.Container.Configure(config =>
            {
                config.For<IDataProtectionProvider>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use(() => app.GetDataProtectionProvider());
            });
            SmObjectFactory.Container.GetInstance<IApplicationUserManager>().SeedDatabase();

            app.CreatePerOwinContext(() => SmObjectFactory.Container.GetInstance<IApplicationUserManager>());


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SmObjectFactory.Container.GetInstance<IApplicationUserManager>().OnValidateIdentity()
                }
            });

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //    Provider = new CookieAuthenticationProvider
            //    {
            //        OnValidateIdentity = ObjectFactory.Container.GetInstance<IApplicationUserManager>().OnValidateIdentity()
            //    }
            //});



            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromSeconds(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);




            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(
            //    clientId: "296817006664-3md759m7j9ivr6bdq9et2699h8j74nap.apps.googleusercontent.com",
            //    clientSecret: "POHC9fDQ7i06ISZZFl-TsIrZ");


        }
    }
}