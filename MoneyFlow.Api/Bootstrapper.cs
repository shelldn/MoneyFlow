using MoneyFlow.Data;
using MoneyFlow.Data.Models;
using Nancy;
using Nancy.TinyIoc;

namespace MoneyFlow.Api
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer ioc)
        {
            base.ConfigureApplicationContainer(ioc);

            ioc.Register<IRepository<Income>, MongoRepository<Income>>();
        }
    }
}