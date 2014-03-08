using System.Web.Http;
using Microsoft.Owin;
using MoneyFlow.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MoneyFlow.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureWebApi(app);
        }
    }
}
