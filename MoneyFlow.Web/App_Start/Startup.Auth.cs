using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace MoneyFlow.Web
{
    public partial class Startup
    {
        private static void ConfigureAuth(IAppBuilder app)
        {
            var srvConfig = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(4),
                Provider = new MoneyFlowAuthorizationServerProvider()
            };

            var authConfig = new OAuthBearerAuthenticationOptions();

            // Token generation
            app.UseOAuthAuthorizationServer(srvConfig);
            app.UseOAuthBearerAuthentication(authConfig);
        }
    }
}