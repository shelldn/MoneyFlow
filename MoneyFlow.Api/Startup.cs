using Owin;
using Microsoft.Owin;
using MoneyFlow.Api;

[assembly: OwinStartup(typeof(Startup))]

namespace MoneyFlow.Api
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}