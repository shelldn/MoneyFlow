using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public partial class MoneyFlowUserStore : IUserSecurityStampStore<Account, Int32>
    {
        public Task SetSecurityStampAsync(Account user, string stamp)
        {
            _db.Entry(user).Property(a => a.SecurityStamp)
                .CurrentValue = stamp;

            return _db.SaveChangesAsync();
        }

        public Task<string> GetSecurityStampAsync(Account user)
        {
            return Task.FromResult(user.SecurityStamp);
        }
    }
}