using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MoneyFlow.Identity
{
    public class MoneyFlowUserStore : IUserStore<IUser>
    {
        public Task CreateAsync(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<IUser> FindByIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IUser> FindByNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}