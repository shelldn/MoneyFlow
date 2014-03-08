using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Owin;

namespace MoneyFlow.Web
{
    public partial class Startup
    {
        private static void ConfigureFormatters(HttpConfiguration config)
        {
            var formatters = config.Formatters;

            // remove xml formatter
            var xmlFormatter = formatters.XmlFormatter;
            formatters.Remove(xmlFormatter);

            // configure json camelCasing
            var jsonFormatter = formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureFormatters(config);
            RegisterRoutes(config);
            IocConfig.RegisterIoc(config);

            app.UseWebApi(config);
        }
    }
}