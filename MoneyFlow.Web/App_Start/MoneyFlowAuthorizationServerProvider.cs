using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

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
            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            
            id.AddClaim(new Claim("name", context.UserName));

            // context.SetError("invalid_grant", "Bad user name or password");
            context.Validated(new ClaimsIdentity(context.Options.AuthenticationType));
        }
    }
}