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
    public class CustomRoleStore : ICustomRoleStore
    {
        private readonly IRoleStore<CustomRole, int> _roleStore;

        public CustomRoleStore(IRoleStore<CustomRole, int> roleStore)
        {
            _roleStore = roleStore;
        }
    }
}
