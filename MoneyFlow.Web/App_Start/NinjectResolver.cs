using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace MoneyFlow.Web
{
    public sealed class NinjectResolver : IDependencyResolver
    {
        public static IKernel Kernel { get; private set; }

        public NinjectResolver(IKernel kernel)
        {
            Kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }
}