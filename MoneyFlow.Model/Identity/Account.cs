using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MoneyFlow.Model
{
    public partial class Account : IUser<int>
    {
        public Account(string email)
        {
            UserName = email;
        }

        public Account(int id, string email) : this(email)
        {
            Id = id;
        }

        public static Account FromLoginInfo(ExternalLoginInfo info)
        {
            return new Account { UserName = info.ExternalIdentity.Name };
        }
    }
}