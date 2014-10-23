using MoneyFlow.Data;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Identity.Ninject;
using Ninject.Modules;

namespace MoneyFlow.Web
{
    public sealed class NinjectModules
    {
        public static INinjectModule[] Modules { get; private set; }

        static NinjectModules()
        {
            Modules = new INinjectModule[]
            {
                new MainModule(),
                new IdentityModule()
            };
        }

        public class MainModule : NinjectModule
        {
            public override void Load()
            {
                Kernel.Bind<IMoneyFlowUow>().To<MoneyFlowUow>();
            }
        }
    }
}