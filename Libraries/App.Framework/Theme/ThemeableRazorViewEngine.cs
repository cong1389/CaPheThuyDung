using System.Web.Mvc;

namespace App.Framework.Theme
{
    public class ThemeableRazorViewEngine : ThemeableVirtualPathProviderViewEngine
    {
        public ThemeableRazorViewEngine()
        {
            AreaViewLocationFormats = new[] { "~/Areas/Views/{1}/{0}.cshtml", "~/Areas/Views/Shared/{0}.cshtml", "~/Areas/Views/{1}/{0}.cshtml", "~/Areas/Views/Shared/{0}.cshtml" };
            AreaMasterLocationFormats = new[] { "~/Areas/Views/{1}/{0}.cshtml", "~/Areas/Views/Shared/{0}.cshtml", "~/Areas/Views/{1}/{0}.cshtml", "~/Areas/Views/Shared/{0}.cshtml" };
            AreaPartialViewLocationFormats = new[] { "~/Areas/Views/{1}/{0}.cshtml", "~/Areas/Views/Shared/{0}.cshtml", "~/Areas/Views/{1}/{0}.cshtml", "~/Areas/Views/Shared/{0}.cshtml" };
            ViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml", "~/Areas/Admin/Views/{1}/{0}.cshtml", "~/Areas/Admin/Views/Shared/{0}.cshtml" };
            MasterLocationFormats = new[] { "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
            PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml", "~/Areas/Admin/Views/{1}/{0}.cshtml", "~/Areas/Admin/Views/Shared/{0}.cshtml" };
            FileExtensions = new[] { "cshtml" };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, false, FileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(controllerContext, viewPath, masterPath, true, FileExtensions);
        }
    }
}