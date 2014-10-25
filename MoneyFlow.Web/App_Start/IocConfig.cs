using System.Web.Http;
using System.Web.Mvc;
using MoneyFlow.Web.Services;
using Ninject;

using IHttpDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace MoneyFlow.Web
{
    public static class IocConfig
    {
        private static readonly IKernel AppKernel
            = new StandardKernel(NinjectModules.Modules);

        private static readonly IDependencyResolver Resolver;
        private static readonly IHttpDependencyResolver HttpResolver;

        static IocConfig()
        {
            Resolver = new NinjectResolver(AppKernel);
            HttpResolver = new NinjectHttpResolver(AppKernel);
        }

        public static void Register(HttpConfiguration config)
        {
            DependencyResolver.SetResolver(Resolver);
            config.DependencyResolver = HttpResolver;

            ControllerBuilder.Current
                .SetControllerFactory(new NinjectControllerFactory(AppKernel));
        }
    }
}