using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;

namespace MoneyFlow.Web
{
    public class NinjectHttpResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectHttpResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            
        }
    }
}