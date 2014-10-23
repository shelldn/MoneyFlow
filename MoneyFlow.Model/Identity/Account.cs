using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MoneyFlow.Model
{
    public partial class Account : IUser<int>
    {
        public static Account FromLoginInfo(ExternalLoginInfo info)
        {
            return new Account { UserName = info.ExternalIdentity.Name };
        }
    }
}