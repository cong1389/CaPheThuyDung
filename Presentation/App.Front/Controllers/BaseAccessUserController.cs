using System;
using System.Web;
using System.Web.Mvc;
using App.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

namespace App.Front.Controllers
{
    public class BaseAccessUserController :Controller
    {
		protected readonly UserManager<IdentityUser, Guid> UserManager;
        private readonly DpapiDataProtectionProvider _provider = new DpapiDataProtectionProvider();


        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

	    protected BaseAccessUserController()
		{
		}

		protected BaseAccessUserController(UserManager<IdentityUser, Guid> userManager, IIdentityMessageService emailService)
		{
            UserManager = userManager;
		    UserManager.EmailService = emailService;

		    if (_provider != null)
		    {
		        var dataProtector = _provider.Create("ResetPassword");

		        UserManager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser, Guid>(dataProtector);
		    }
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
	}
}