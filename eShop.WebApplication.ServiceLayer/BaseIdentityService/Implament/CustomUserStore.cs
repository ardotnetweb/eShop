using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace eShop.WebApplication.ServiceLayer.BaseIdentityService.Implament
{
    public class CustomUserStore : ICustomUserStore
    {
        private readonly IUserStore<ApplicationUser, int> _userStore;
        public CustomUserStore(IUserStore<ApplicationUser, int> userStore)
        {
            _userStore = userStore;
        }
    }
}
