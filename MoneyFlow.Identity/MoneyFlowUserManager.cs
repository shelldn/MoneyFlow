using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public class MoneyFlowUserManager
    {
        public static UserManager<Account, int> Create()
        {
            var mgr = new UserManager<Account, int>(new MoneyFlowUserStore());

            mgr.UserValidator = new UserValidator<Account, int>(mgr)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            return mgr;
        }
    }
}