using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MoneyFlow.Web.Startup))]

namespace MoneyFlow.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.RegisterRoutes(config);
            WebApiConfig.ConfigureFormatters(config.Formatters);
            IocConfig.RegisterIoc(config);

            app.UseWebApi(config);
        }
    }
}
