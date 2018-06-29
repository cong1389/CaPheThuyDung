using App.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Admin.Helpers
{
	public class RequiredPermisson : AuthorizeAttribute
	{
		private UserManager<IdentityUser, Guid> userManager
		{
			get
			{
				return DependencyResolver.Current.GetService<UserManager<IdentityUser, Guid>>();
			}
		}

		public RequiredPermisson()
		{
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (!httpContext.User.Identity.IsAuthenticated)
			{
				return false;
			}
			string name = httpContext.User.Identity.Name;
			if (this.userManager.FindByName<IdentityUser, Guid>(name).IsSuperAdmin)
			{
				return true;
			}
			if (httpContext.User.IsInRole(base.Roles))
			{
				return true;
			}
			return false;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Security", action = "AccessDined", ReturnUrl = filterContext.HttpContext.Request.Url }));
				return;
			}
			filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login", ReturnUrl = filterContext.HttpContext.Request.Url }));
		}
	}
}