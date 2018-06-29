using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using App.Aplication;
using App.Framework.Theme;

namespace App.Front
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Initialize engine context
            //var engine = EngineContext.Initialize(false);

            Bootstrapper.Run();

            ConfigureViewEngines();

            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        private static void ConfigureAntiForgeryTokens()
        {
            AntiForgeryConfig.CookieName = "f";
        }

        private static void ConfigureViewEngines()
        { 
            // Remove all view engines
            ViewEngines.Engines.Clear();

            //Add Custom C# Razor View Engine  
            //ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
        }

        //public void AnonymousIdentification_Creating(object sender, AnonymousIdentificationEventArgs args)
        //{
        //    try
        //    {
        //        //var customerService = DependencyResolver.Current.GetService<ICustomerService>();
        //        //var customer = customerService.GetByGuid(args.AnonymousID.ToString());
        //        args.AnonymousID = Guid.NewGuid().ToString();
        //    }
        //    catch { }
        //}
    }
}
