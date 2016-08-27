using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.WebApplication.DomainClasses.AppClasses;

namespace eShop.WebApplication.DomainClasses.ViewModelClasses
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostallCode { get; set; }
        public string ReciveMessage { get; set; }
    }
    public class UserViewModelProfile
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostallCode { get; set; }
        public string Email { get; set; }
        public bool StatusDisable { get; set; }
        public DateTime DateDisable { get; set; }
        public bool ReciveMessage { get; set; }
    }
}
