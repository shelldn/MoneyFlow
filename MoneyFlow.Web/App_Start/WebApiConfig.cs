using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MoneyFlow.Web
{
    public static class WebApiConfig
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void ConfigureFormatters(MediaTypeFormatterCollection formatters)
        {
            // remove xml formatter
            var xmlFormatter = formatters.XmlFormatter;
            formatters.Remove(xmlFormatter);

            // configure json camelCasing
            var jsonFormatter = formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }
    }
}