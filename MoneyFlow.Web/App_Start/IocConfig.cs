using System.Web.Http;
using MoneyFlow.Data;
using MoneyFlow.Data.Contracts;
using Ninject;

namespace MoneyFlow.Web
{
    public class IocConfig
    {
        public static void RegisterIoc(HttpConfiguration config)
        {
            var kernel = new StandardKernel();

            // setup bindings
            kernel.Bind<IMoneyFlowUow>().To<MoneyFlowUow>();

            // setup dependency resolver
            config.DependencyResolver = new NinjectDependencyResolver(kernel);
        }
    }
}