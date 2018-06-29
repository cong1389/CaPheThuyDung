using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace App.Front
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(new Action<HttpConfiguration>(WebApiConfig.Register));
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            app.MapSignalR();
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            //app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<IdentityConfig>());

            string loginUrl = new UrlHelper(HttpContext.Current.Request.RequestContext).Action("Login", "User", new { area = "" });

            // Enable the application to use a cookie to store
            // information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType =DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(loginUrl)
            });

            // Use a cookie to temporarily store information about
            // a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.UseGoogleAuthentication(
                "34554057736-s8tbm06iemdbsas0i8osrvu3du3jjk6k.apps.googleusercontent.com",
                clientSecret: "xga3DkrBRSJOLSMd18AJMTOW");

            // Uncomment the following lines to enable logging in
            // with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            // clientId: "",
            // clientSecret: "");

            //app.UseTwitterAuthentication(
            // consumerKey: "",
            // consumerSecret: "");

            app.UseFacebookAuthentication(
             "1318503118251337",
             appSecret: "2b816c9bb9637eddfb8056aaca4abe1b");

        }
    }
}