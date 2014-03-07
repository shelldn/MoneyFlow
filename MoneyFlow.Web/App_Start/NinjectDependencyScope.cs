using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Ninject.Parameters;
using Ninject.Syntax;

namespace MoneyFlow.Web
{
    public class NinjectDependencyScope : IDependencyScope
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyScope(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        public object GetService(Type serviceType)
        {
            return GetServices(serviceType).FirstOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var request = _resolutionRoot.CreateRequest(serviceType, null, new IParameter[0], true, true);
            return _resolutionRoot.Resolve(request);
        }

        public void Dispose()
        {
            var disposableRoot = _resolutionRoot as IDisposable;
            if (disposableRoot != null) disposableRoot.Dispose();
        }
    }
}