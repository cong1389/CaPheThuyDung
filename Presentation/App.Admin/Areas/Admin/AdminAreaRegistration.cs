using System.Web.Mvc;

namespace App.Admin.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Admin_default", "Admin/{controller}/{action}/{id}", new { controller = "Home", action = "Index", area = "Admin", id = UrlParameter.Optional }, new string[] { "App.Admin.Controllers" });
            context.MapRoute("Admin_DefaultPaging", "Admin/{controller}/{action}/page-{page}", new { action = "Index", area = "Admin", page = UrlParameter.Optional }, new string[] { "App.Admin.Controllers" });

            //context.MapRoute(
            //    "Admin_default",
            //    "Admin/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}