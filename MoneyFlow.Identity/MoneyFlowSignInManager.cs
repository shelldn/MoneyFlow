using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public class MoneyFlowSignInManager : SignInManager<Account, int>
    {
        public MoneyFlowSignInManager(
            UserManager<Account, int> userManager, 
            IAuthenticationManager authManager)
            
            : base(userManager, authManager) { }
    }
}