using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MoneyFlow.Identity;
using MoneyFlow.Model;
using MoneyFlow.Web.ViewModels;

namespace MoneyFlow.Web.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        protected string CallbackUri { get; set; }

        public IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public UserManager<Account, Int32> UserManager
        {
            get
            {
                var um = new UserManager<Account, int>(new MoneyFlowUserStore());

                um.UserValidator = new UserValidator<Account, Int32>(um)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                return um;
            }
        }

        public MoneyFlowSignInManager SignInManager
        {
            get 
            { 
                return new MoneyFlowSignInManager(
                    new UserManager<Account, int>(new MoneyFlowUserStore()), 
                    AuthManager
                );
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // set the return Uri for the
            // facebook authentication middleware
            CallbackUri = Url.Action("SignInExternalCallback");
        }

        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginViewModel model, string returnUrl)
        {
            throw new NotImplementedException();
        }

        [ActionName("sign-out")]
        public ActionResult SignOut()
        {
            AuthManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #region External auth impl.

        protected class ChallengeResult : ActionResult
        {
            protected readonly AuthenticationProperties AuthConfig;

            public ChallengeResult(string redirectUri)
            {
                AuthConfig = new AuthenticationProperties { RedirectUri = redirectUri };
            }

            private static IAuthenticationManager GetAuthManager(ControllerContext context)
            {
                return context.HttpContext
                    .GetOwinContext()
                    .Authentication;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                GetAuthManager(context).Challenge(AuthConfig, "Facebook");
            }
        }

        protected ActionResult Challenge(string redirectUri)
        {
            return new ChallengeResult(redirectUri);
        }

        [Route("sign-in/facebook")]
        public ActionResult SignInExternal()
        {
            return Challenge(redirectUri: CallbackUri);
        }

        [Route("sign-in/facebook/callback")]
        public async Task<ActionResult> SignInExternalCallback()
        {
            /* 
             * TODO: Refactor this, please!.. 
             * 
             */

            var li = await AuthManager.GetExternalLoginInfoAsync();
            var siResult = await SignInManager.ExternalSignInAsync(li, isPersistent: false);

            switch (siResult)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.Failure:
                default:
                    var account = Account.FromLoginInfo(info: li);
                    var result = await UserManager.CreateAsync(account);

                    if (result.Succeeded)
                    {
                        result = await UserManager.AddLoginAsync(account.Id, li.Login);

                        if (result.Succeeded)
                            await SignInManager.SignInAsync(account, false, false);
                    }

                    return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}