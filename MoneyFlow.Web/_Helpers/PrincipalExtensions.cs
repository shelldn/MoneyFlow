using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace MoneyFlow.Web
{
    public static class PrincipalExtensions
    {
        public static int GetId(this IPrincipal user)
        {
            return user.Identity.GetUserId<int>();
        }
    }
}