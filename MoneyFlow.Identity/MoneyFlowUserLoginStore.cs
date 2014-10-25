using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public sealed partial class MoneyFlowUserStore : IUserLoginStore<Account, Int32>
    {
        public Task AddLoginAsync(Account user, UserLoginInfo login)
        {
            _db.Accounts.Attach(user)
                .Logins.Add(new Login
                {
                    LoginProvider = login.LoginProvider,
                    ProviderKey = login.ProviderKey
                });

            return _db.SaveChangesAsync();
        }

        public Task RemoveLoginAsync(Account user, UserLoginInfo login)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(Account user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Account> FindAsync(UserLoginInfo loginInfo)
        {
            return await _db.Set<Login>()
                .Include(l => l.Account)
                .SingleOrDefaultAsync(l =>
                    l.LoginProvider == loginInfo.LoginProvider &&
                    l.ProviderKey == loginInfo.ProviderKey)

                    .ContinueWith(t => t.Result != null ? t.Result.Account : null);
        }
    }
}