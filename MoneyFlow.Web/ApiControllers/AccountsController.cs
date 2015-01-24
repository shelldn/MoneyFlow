using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Web.ApiControllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        public UserManager<Account, Int32> UserManager { get; protected set; }

        public AccountsController(UserManager<Account, Int32> userManager)
        {
            UserManager = userManager;
        }

        [Route("me")]
        public Account GetCurrent()
        {
            var id = User.Identity
                .GetUserId<Int32>();

            return UserManager.FindById(id);
        }
    }
}