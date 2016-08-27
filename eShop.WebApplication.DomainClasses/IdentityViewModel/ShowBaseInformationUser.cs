using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.IdentityViewModel
{
    public class ShowBaseInformationUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool StatusDisableUser { get; set; }
        public bool ReciveMessage { get; set; }
    }
}
