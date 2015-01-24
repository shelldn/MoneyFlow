using Microsoft.Owin;
using MoneyFlow.Web;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MoneyFlow.Web
{
    public partial class Startup
    {
        private static void RegisterGlobals(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new StandardKernel(NinjectModules.Modules) as IKernel);
        }

        public void Configuration(IAppBuilder app)
        {
            RegisterGlobals(app);
            ConfigureAuth(app);
        }
    }
}
