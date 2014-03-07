using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace MoneyFlow.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}