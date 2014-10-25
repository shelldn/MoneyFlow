using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace MoneyFlow.Web.Services
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        protected readonly IKernel Kernel;

        public NinjectControllerFactory(IKernel kernel)
        {
            Kernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return Kernel.TryGet(controllerType) as IController;
        }
    }
}