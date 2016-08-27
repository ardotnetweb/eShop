using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.ViewModelClasses;

namespace eShop.WebApplication.DomainClasses.IdentityViewModel
{
    public class ShowInformationUserViewModel
    {
        public UpdateProfileUserViewModel ApplicationUser { get; set; }
        public IList<string> RolesUser { get; set; }
    }
}
