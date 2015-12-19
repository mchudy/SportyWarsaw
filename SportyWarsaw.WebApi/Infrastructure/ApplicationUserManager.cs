using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using SportyWarsaw.Domain.Entities;
using System;

namespace SportyWarsaw.WebApi.Infrastructure
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store, IDataProtectionProvider dataProtectionProvider)
          : base(store)
        {
            // Configure validation logic for usernames
            UserValidator = new UserValidator<User>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            //RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            //EmailService = new EmailService();
            //SmsService = new SmsService();

            if (dataProtectionProvider != null)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }
}
