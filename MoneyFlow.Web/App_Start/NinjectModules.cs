using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MoneyFlow.Data;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Identity;
using MoneyFlow.Model;
using Ninject;
using Ninject.Activation;
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

        private sealed class MainModule : NinjectModule
        {
            public override void Load()
            {
                Kernel.Bind<IMoneyFlowUow>().To<MoneyFlowUow>();
            }
        }

        private sealed class IdentityModule : NinjectModule
        {
            private class MoneyFlowUserManagerProvider : Provider<UserManager<Account, Int32>>
            {
                protected override UserManager<Account, int> CreateInstance(IContext context)
                {
                    var store = context.Kernel
                        .Get<IUserStore<Account, Int32>>();

                    var mgr = new UserManager<Account, Int32>(store)
                    {
                        EmailService = new MoneyFlowMailService()
                    };

                    mgr.UserValidator = new UserValidator<Account, Int32>(mgr)
                    {
                        // allow spaces & co. in user names
                        AllowOnlyAlphanumericUserNames = false
                    };

                    return mgr;
                }
            }

            public override void Load()
            {
                Kernel.Bind<IAuthenticationManager>()
                    .ToMethod(c => HttpContext.Current.GetOwinContext().Authentication);

                Kernel.Bind<SignInManager<Account, Int32>>().To<MoneyFlowSignInManager>();

                Kernel.Bind<IUserStore<Account, int>>().To<MoneyFlowUserStore>();

                Kernel.Bind<UserManager<Account, int>>()
                    .ToProvider(new MoneyFlowUserManagerProvider());
            }
        }
    }
}