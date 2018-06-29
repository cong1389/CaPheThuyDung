using System.Web.Mvc;

namespace App.Admin.Controllers
{
	public class SecurityController : Controller
	{
	    public ActionResult AccessDined(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
	}
}