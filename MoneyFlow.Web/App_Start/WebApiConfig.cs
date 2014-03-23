using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MoneyFlow.Web
{
    public class WebApiConfig
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

        public static void Register(HttpConfiguration config)
        {
            ConfigureFormatters(config);
            RegisterRoutes(config);
        }
    }
}