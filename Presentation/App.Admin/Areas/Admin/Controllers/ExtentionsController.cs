using System.Web.Mvc;

namespace App.Admin.Controllers
{
	public class ExtentionsController : BaseAdminController
	{
	    public ActionResult ResourceScript()
		{
			Response.ContentType = "text/javascript";
			return View();
		}
	}
}