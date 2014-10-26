using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public partial class MoneyFlowUserStore : IUserPasswordStore<Account, Int32>
    {
        public Task SetPasswordHashAsync(Account user, string passwordHash)
        {
            _db.Entry(user).Property(a => a.PasswordHash)
                .CurrentValue = passwordHash;

            return _db.SaveChangesAsync();
        }

        public Task<string> GetPasswordHashAsync(Account user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Account user)
        {
            throw new NotImplementedException();
        }
    }
}