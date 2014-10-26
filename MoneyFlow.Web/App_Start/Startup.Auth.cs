using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;

namespace MoneyFlow.Web
{
    public partial class Startup
    {
        private static void ConfigureAuth(IAppBuilder app)
        {
            var options = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/signin"),
                CookieName = "mf_auth"
            };

            app.UseCookieAuthentication(options);

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "314296758754120",
                AppSecret = "f56188159a2577b73c91858bec90fc5e"
            });
        }
    }
}