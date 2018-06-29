using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Aplication;
using App.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace App.Admin.Controllers
{
    public abstract class BaseDefaultIdentity : BaseAdminController
    {
        protected readonly UserManager<IdentityUser, Guid> UserManager;

        protected string XsrfKey = Contains.XsrfKey;

        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        protected BaseDefaultIdentity()
        {
        }

        protected BaseDefaultIdentity(UserManager<IdentityUser, Guid> userManager)
        {
            UserManager = userManager;
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected Guid GetGuid(string value)
        {
            Guid.TryParse(value, out var guid);

            return guid;
        }

        protected IList<AuthenticationDescription> GetUnassignedExternalLogins(IList<UserLoginInfo> userLogins)
        {
            return (
                from auth in AuthenticationManager.GetAuthenticationTypes()
                where userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)
                select auth).ToList();
        }

        protected bool HasPassword()
        {
            var identityUser = UserManager.FindById(GetGuid(User.Identity.GetUserId()));

            return identityUser?.PasswordHash != null;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
    }
}