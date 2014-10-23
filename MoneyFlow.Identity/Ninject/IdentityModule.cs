using Microsoft.AspNet.Identity;
using MoneyFlow.Model;
using Ninject.Modules;

namespace MoneyFlow.Identity.Ninject
{
    public class IdentityModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUserStore<Account, int>>().To<MoneyFlowUserStore>();
            Kernel.Bind<UserManager<Account, int>>().ToMethod(NinjectUserManagerFactory.Create);
        }
    }
}