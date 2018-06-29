using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Admin.Helpers
{
	public class AuthorizeCustom : AuthorizeAttribute
	{
		private string _notifyUrl = "/User/Login";

		public string NotifyUrl
		{
			get
			{
				return this._notifyUrl;
			}
			set
			{
				this._notifyUrl = value;
			}
		}

		public AuthorizeCustom()
		{
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login", ReturnUrl = filterContext.HttpContext.Request.Url }));
			}
		}
	}
}