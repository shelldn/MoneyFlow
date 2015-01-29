using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using MoneyFlow.Model;
using Ninject;

namespace MoneyFlow.Web
{
    public class MoneyFlowAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var kernel = context.OwinContext.Get<IKernel>();
            var mgr = kernel.Get<UserManager<Account, Int32>>();

            var account = mgr.Find(context.UserName, context.Password);
            var id = mgr.CreateIdentity(account, context.Options.AuthenticationType);

            context.Validated(id);
        }
    }
}