using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Identity
{
    public sealed partial class MoneyFlowUserStore : IUserStore<Account, Int32>
    {
        private readonly MoneyFlowDbContext _db =
            new MoneyFlowDbContext();

        #region IUserStore impl.

        public Task CreateAsync(Account user)
        {
            _db.Accounts.Add(user);

            return _db
                .SaveChangesAsync();
        }

        public Task UpdateAsync(Account user)
        {
            _db.Entry(user).State = EntityState.Modified;

            return _db.SaveChangesAsync();
        }

        public Task DeleteAsync(Account user)
        {
            throw new System.NotImplementedException();
        }

        public Task<Account> FindByIdAsync(int userId)
        {
            return _db.Accounts
                .FindAsync(userId);
        }

        public Task<Account> FindByNameAsync(string userName)
        {
            return _db.Set<Account>()
                .SingleOrDefaultAsync(a => a.UserName == userName);
        }

        #endregion

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}