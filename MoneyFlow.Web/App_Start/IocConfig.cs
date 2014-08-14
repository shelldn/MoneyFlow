using System.Web.Http;

namespace MoneyFlow.Web
{
    public class IocConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new NinjectHttpResolver(NinjectHttpModules.Modules);
        }
    }
}