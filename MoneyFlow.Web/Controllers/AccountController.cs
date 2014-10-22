using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        public IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public UserManager<Account, Int32> UserManager
        {
            get { return new UserManager<Account, int>(new MoneyFlowUserStore()); }
        }

        public MoneyFlowSignInManager SignInManager
        {
            get 
            { 
                return new MoneyFlowSignInManager(
                    new UserManager<Account, int>(new MoneyFlowUserStore()), 
                    AuthenticationManager
                ); 
            }
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
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #region External auth impl.

        protected class ChallengeResult : ActionResult
        {
            protected readonly AuthenticationProperties AuthConfig;

            public ChallengeResult(string redirectUri)
            {
                AuthConfig = new AuthenticationProperties
                {
                    RedirectUri = redirectUri
                };
            }

            public override void ExecuteResult(ControllerContext context)
            {
                context.HttpContext.GetOwinContext().Authentication
                    .Challenge(AuthConfig, "Facebook");
            }
        }

        protected ActionResult Challenge(string redirectUri)
        {
            return new ChallengeResult(redirectUri);
        }

        [Route("sign-in/facebook")]
        public ActionResult SignInExternal()
        {
            return Challenge(Url.Action("SignInExternalCallback"));
        }

        [Route("sign-in/facebook/callback")]
        public async Task<ActionResult> SignInExternalCallback()
        {
            /* 
             * TODO: Refactor this, please!.. 
             * 
             */

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            var siResult = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);

            switch (siResult)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.Failure:
                default:
                    var userName = loginInfo.DefaultUserName;
                    var account = new Account { UserName = userName };

                    var result = await UserManager.CreateAsync(account);

                    if (result.Succeeded)
                    {
                        result = await UserManager.AddLoginAsync(account.Id, loginInfo.Login);

                        if (result.Succeeded)
                            await SignInManager.SignInAsync(account, false, false);
                    }

                    return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}