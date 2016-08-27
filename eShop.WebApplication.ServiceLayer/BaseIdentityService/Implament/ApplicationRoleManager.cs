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
    public class ApplicationRoleManager : RoleManager<CustomRole, int>, IApplicationRoleManager
    {
        private readonly IRoleStore<CustomRole, int> _roleStore;
        public ApplicationRoleManager(IRoleStore<CustomRole, int> roleStore)
            : base(roleStore)
        {
            _roleStore = roleStore;
        }


        public CustomRole FindRoleByName(string roleName)
        {
            return this.FindByName(roleName); // RoleManagerExtensions
        }

        public IdentityResult CreateRole(CustomRole role)
        {
            return this.Create(role); // RoleManagerExtensions
        }
    }

}
