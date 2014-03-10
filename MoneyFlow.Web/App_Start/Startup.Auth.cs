using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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
                LoginPath = new PathString("/account/signin")
            };

            app.UseCookieAuthentication(options);
        }
    }
}