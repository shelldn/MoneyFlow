using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MoneyFlow.Web
{
    public class WebApiConfig
    {
        private static void ConfigureFormatters(HttpConfiguration config)
        {
            var formatters = config.Formatters;

            var xmlFormatter = 
                formatters.XmlFormatter;

            var jsonSettings =

                formatters.JsonFormatter
                    .SerializerSettings;

            // remove xml formatter
            formatters.Remove(xmlFormatter);

            // use ISO 8601 date format for JSON APIs
            jsonSettings.Converters.Add(new IsoDateTimeConverter());

            // use local time
            jsonSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            // configure json camelCasing
            jsonSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // ignore self references
            jsonSettings.ReferenceLoopHandling = 
                ReferenceLoopHandling.Ignore;
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
            IocConfig.Register(config);
        }
    }
}