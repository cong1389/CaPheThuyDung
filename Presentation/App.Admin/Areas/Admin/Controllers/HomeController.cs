using System.Web.Mvc;
using App.Admin.Helpers;

namespace App.Admin.Controllers
{
	[AuthorizeCustom]
	public class HomeController : BaseAdminController
	{
	    public ActionResult Index()
		{
			return View();
		}
	}
}