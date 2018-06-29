using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using App.Domain.Entities.Identity;
using App.FakeEntity.User;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Resources;

namespace App.Admin.Controllers
{
	public class UserController : BaseDefaultIdentity
	{
		public UserController(UserManager<IdentityUser, Guid> userManager) : base(userManager)
		{
		}

		public ActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			ActionResult actionResult;
			var flag = HasPassword();
			ViewBag.HasLocalPassword = flag;
			ViewBag.ReturnUrl = Url.Action("Index", "Home");
			if (!flag)
			{
				var item = ModelState["OldPassword"];
				if (item != null)
				{
					item.Errors.Clear();
				}
				if (ModelState.IsValid)
				{
					var identityResult = await UserManager.AddPasswordAsync(GetGuid(User.Identity.GetUserId()), model.NewPassword);
					var identityResult1 = identityResult;
					if (!identityResult1.Succeeded)
					{
						AddErrors(identityResult1);
					}
					else
					{
						Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.Password)));
						actionResult = View();
						return actionResult;
					}
				}
			}
			else if (ModelState.IsValid)
			{
				var identityResult2 = await UserManager.ChangePasswordAsync(GetGuid(User.Identity.GetUserId()), model.OldPassword, model.NewPassword);
				var identityResult3 = identityResult2;
				if (!identityResult3.Succeeded)
				{
					AddErrors(identityResult3);
				}
				else
				{
					Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.ErrorMessageWithFormat, FormUI.Password)));
					actionResult = View();
					return actionResult;
				}
			}
			actionResult = View();
			return actionResult;
		}

		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel login, string returnUrl)
		{
			ActionResult action;
			if (ModelState.IsValid)
			{
				var identityUser = await UserManager.FindAsync(login.UserName, login.Password);
				var identityUser1 = identityUser;
				if (identityUser1 == null)
				{
					ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
				}
				else
				{
					await SignInAsync(identityUser1, login.Remember);
					if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
					{
						action = RedirectToAction("Index", "Home");
						return action;
					}

				    action = Redirect(returnUrl);
				    return action;
				}
			}
			action = View();
			return action;
		}

		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut();
			return RedirectToAction("Login");
		}

		private async Task SignInAsync(IdentityUser user, bool isPersistent)
		{
			AuthenticationManager.SignOut("ExternalCookie");
			var claimsIdentity = await UserManager.CreateIdentityAsync(user, "ApplicationCookie");
			var authenticationManager = AuthenticationManager;
			var authenticationProperty = new AuthenticationProperties
			{
				IsPersistent = isPersistent
			};
			authenticationManager.SignIn(authenticationProperty, claimsIdentity);
		}
	}
}