using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public partial class MoneyFlowUserStore : IUserTwoFactorStore<Account, Int32>
    {
        public Task SetTwoFactorEnabledAsync(Account user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(Account user)
        {
            return Task.FromResult(false);
        }
    }
}