using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Modules;

namespace MoneyFlow.Web
{
    public class NinjectHttpResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectHttpResolver(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(modules);
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