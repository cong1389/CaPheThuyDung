using System;
using App.Domain.Entities.Identity;
using App.Front.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace App.Front.App_Start
{
    public class IdentityConfig : UserManager<IdentityUser, Guid>
    {
        public IdentityConfig(IUserStore<IdentityUser, Guid> store, IIdentityMessageService emailService, IDataProtectionProvider dataProtectionProvider) : base(store)
        {
            // Configure validation logic for usernames
            UserValidator = new UserValidator<IdentityUser, Guid>(this)
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
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<IdentityUser, Guid>
            {
                MessageFormat = "Your security code is {0}"
            });
            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<IdentityUser, Guid>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            EmailService = emailService;
            //this.SmsService = new SmsService();

            if (dataProtectionProvider != null)
            {
                IDataProtector dataProtector = dataProtectionProvider.Create("ResetPassword");

                UserTokenProvider = new DataProtectorTokenProvider<IdentityUser, Guid>(dataProtector);
            }

            //alternatively use this if you are running in Azure Web Sites
            UserTokenProvider = new EmailTokenProvider<IdentityUser, Guid>();
        }
    }
}