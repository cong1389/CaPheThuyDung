using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Front.Models
{
	public class FrontAuthorize : AuthorizeAttribute
	{
	    public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
				{
				    controller = "User",
				    action = "Login",
				    area = "",
				    ReturnUrl = filterContext.HttpContext.Request.Url
				}));
			}
		}
	}
}