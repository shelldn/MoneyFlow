using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public partial class MoneyFlowUserStore : IUserLockoutStore<Account, Int32>
    {
        public Task<DateTimeOffset> GetLockoutEndDateAsync(Account user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(Account user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(Account user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(Account user)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(Account user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(Account user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(Account user, bool enabled)
        {
            throw new NotImplementedException();
        }
    }
}