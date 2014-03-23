using MoneyFlow.Data;
using MoneyFlow.Data.Contracts;
using Ninject.Modules;

namespace MoneyFlow.Web
{
    public sealed class NinjectHttpModules
    {
        public static INinjectModule[] Modules { get; private set; }

        static NinjectHttpModules()
        {
            Modules = new[] { new MainModule() };
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