using eShop.WebApplication.DomainClasses.AppClasses;
using eShop.WebApplication.DomainClasses.IdentityViewModel;
using eShop.WebApplication.DomainClasses.ViewModelClasses;
using eShop.WebApplication.ServiceLayer.BaseIdentityService.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.BaseIdentityService.Implament
{
    public class ApplicationUserManager
        : UserManager<ApplicationUser, int>, IApplicationUserManager
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUserStore<ApplicationUser, int> _store;

        public ApplicationUserManager(IUserStore<ApplicationUser, int> store,
                                      IApplicationRoleManager roleManager,
                                      IDataProtectionProvider dataProtectionProvider,
                                      IIdentityMessageService smsService,
                                      IIdentityMessageService emailService)
            : base(store)
        {
            _store = store;
            _roleManager = roleManager;
            _dataProtectionProvider = dataProtectionProvider;
            this.SmsService = smsService;
            this.EmailService = emailService;

            createApplicationUserManager();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<bool> HasPassword(int userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PasswordHash != null;
        }

        public async Task<bool> HasPhoneNumber(int userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PhoneNumber != null;
        }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(
                         validateInterval: TimeSpan.FromMinutes(30),
                         regenerateIdentityCallback: (manager, user) => generateUserIdentityAsync(manager, user),
                         getUserIdCallback: id => id.GetUserId<int>());
        }

        public void SeedDatabase()
        {

            const string name = "aroshanzamir@eramWeb.com";
            const string password = "Ar2480014037";

            //const string roleName = "Admin";

            const string privateName = "private";
            const string protectName = "protect";
            const string publicName = "public";

            var privateRole = _roleManager.FindRoleByName(privateName);
            var protectRole = _roleManager.FindRoleByName(protectName);
            var publicRole = _roleManager.FindRoleByName(publicName);

            if (privateRole == null)
            {
                privateRole = new CustomRole(privateName);
                var privateResult = _roleManager.CreateRole(privateRole);
            }
            if (protectRole == null)
            {
                protectRole = new CustomRole(protectName);
                var protectResult = _roleManager.CreateRole(protectRole);
            }
            if (publicRole == null)
            {
                publicRole = new CustomRole(publicName);
                var publicResult = _roleManager.CreateRole(publicRole);
            }


            //Create Role Admin if it does not exist
            var user = this.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = name,
                    Email = name,
                    EmailConfirmed = true,
                    RegisterDate = DateTime.Now.Date,
                    DateDisableUser = DateTime.Parse("2001/1/1")
                };
                var result = this.Create(user, password);
                result = this.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = this.GetRoles(user.Id);
            if (!rolesForUser.Contains(privateRole.Name))
            {
                var result = this.AddToRole(user.Id, privateRole.Name);
            }
        }

        private void createApplicationUserManager()
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = false;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Your security code is: {0}"
            });
            this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });

            if (_dataProtectionProvider != null)
            {
                var dataProtector = _dataProtectionProvider.Create("ASP.NET Identity");
                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtector);
            }
        }

        private async Task<ClaimsIdentity> generateUserIdentityAsync(ApplicationUserManager manager, ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
        public async Task<UpdateProfileUserViewModel> FindUserById(int Id)
        {
            var user = await this.FindByIdAsync(Id);
            if (user != null)
            {
                var profileUser = new UpdateProfileUserViewModel();
                if (user.Address != null)
                {
                    profileUser.Address = user.Address.Address;
                    profileUser.PostalCode = user.Address.PostalCode;
                    profileUser.City_Id = user.Address.City.Id;
                    profileUser.Province_Id = user.Address.Province.Id;
                    profileUser.Province = user.Address.Province.Name;
                    profileUser.City = user.Address.City.Name;
                }
                profileUser.ReciveMessage = user.ReciveMessage;
                profileUser.Name = user.Name;
                profileUser.Family = user.Family;
                profileUser.Id = user.Id;
                profileUser.Phone = user.PhoneNumber;
                profileUser.Email = user.Email;
                profileUser.StatusDisableUser = user.DisableUser;
                profileUser.DateDisableUser = user.DateDisableUser;

                return profileUser;
            }
            else
                return null;
        }
        
        public List<ShowBaseInformationUser> GetAllPaging(string term, int page, int count)
        {
            var list = this.Users.Where(x => x.UserName.Contains(term) || x.Name.Contains(term)
                || x.Family.Contains(term)).Select(x => new ShowBaseInformationUser
            {
                Id = x.Id,
                Name = x.Name,
                Family = x.Family,
                ReciveMessage = x.ReciveMessage,
                UserName = x.UserName,
                Phone = x.PhoneNumber,
                StatusDisableUser = x.DisableUser
            }).OrderBy(x => x.Id).Skip(count * page).Take(count).ToList();
            return list;
        }
        
        public async Task<UserViewModel> FindUserByIdForShow_(int Id)
        {
            var user = await this.FindByIdAsync(Id);
            if (user != null)
            {
                var _user = new UserViewModel();
                if (user.Address != null)
                {
                    _user.Address = user.Address.Address;
                    _user.City = user.Address.City.Name;
                    _user.Province = user.Address.Province.Name;
                    _user.PostallCode = user.Address.PostalCode;
                }
                _user.Name = user.Name;
                _user.Family = user.Family;
                _user.Phone = user.PhoneNumber;
                _user.ReciveMessage = user.ReciveMessage ? "بلی" : "خیر";
                return _user;
            }
            else
                return null;
        }
    }
}
