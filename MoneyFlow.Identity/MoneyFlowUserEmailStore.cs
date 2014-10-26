using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public partial class MoneyFlowUserStore : IUserEmailStore<Account, Int32>
    {
        public Task SetEmailAsync(Account user, string email)
        {
            _db.Entry(user).Property(a => a.UserName)
                .CurrentValue = email;

            return _db.SaveChangesAsync();
        }

        public Task<string> GetEmailAsync(Account user)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> GetEmailConfirmedAsync(Account user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(Account user, bool confirmed)
        {
            _db.Entry(user).Property(a => a.EmailConfirmed)
                .CurrentValue = confirmed;

            return _db.SaveChangesAsync();
        }

        public Task<Account> FindByEmailAsync(string email)
        {
            return _db.Set<Account>()
                .SingleOrDefaultAsync(a => a.UserName == email);
        }
    }
}