using System.Web.Optimization;

namespace App.Front
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/script_jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        , "~/Scripts/jquery.validate*"));
            bundles.Add(new StyleBundle("~/bundles/content_jquery").Include(
                "~/Content/jquery-ui.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.jquery-uifancyBox
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            ////bootstrap
            bundles.Add(new StyleBundle("~/bundles/content_bootstrap").Include(
                "~/Content/bootstrap.css"
               , "~/Content/font-awesome.*"));
            bundles.Add(new ScriptBundle("~/bundles/script_bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //fancybox
            bundles.Add(new StyleBundle("~/bundles/content_fancybox").Include(
                "~/Content/jquery.fancybox.*"));
            bundles.Add(new ScriptBundle("~/bundles/script_fancybox").Include(
                "~/Scripts/jquery.fancybox.*"));

            //Slide
            bundles.Add(new StyleBundle("~/bundles/content_bxslider").Include(
                "~/Content/bxslider/css/jquery.bxslider*"));
            bundles.Add(new ScriptBundle("~/bundles/script_bxslider").Include(
                "~/Content/bxslider/js/jquery.bxslider*"));

            //Carousel
            bundles.Add(new StyleBundle("~/bundles/content_carousel").Include(
                "~/Content/owl.*"));
            bundles.Add(new ScriptBundle("~/bundles/script_carousel").Include(
                "~/Scripts/owl.*"));

            //Modernizr
            bundles.Add(new ScriptBundle("~/bundles/script_modernizr").Include(
                "~/Scripts/modernizr*"));

            //Cookie
            bundles.Add(new ScriptBundle("~/bundles/script_cookie").Include(
                "~/Scripts/jquery.cookie*"));

            //Animate
            bundles.Add(new StyleBundle("~/bundles/content_animate").Include(
                "~/Content/animate*"));

            //Theme
            bundles.Add(new StyleBundle("~/bundles/content_theme").Include(
                "~/Themes/Basic/Content/theme.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/script_theme").Include(
                "~/Themes/Basic/Scripts/goweb.*"));

            //TODO
            BundleTable.EnableOptimizations = true;
//#if DEBUG
//            BundleTable.EnableOptimizations = false;
//#else
//        BundleTable.EnableOptimizations = true;
//#endif

        }
    }
}
