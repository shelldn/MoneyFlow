using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MoneyFlow.Model;
using MoneyFlow.Web.ViewModels;

namespace MoneyFlow.Web.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        protected string CallbackUri { get; set; }

        public IAuthenticationManager AuthManager { get; set; }
        public UserManager<Account, Int32> UserManager { get; set; }
        public SignInManager<Account, Int32> SignInManager { get; set; }

        public AccountController(
            IAuthenticationManager authManager, 
            UserManager<Account, Int32> userManager,
            SignInManager<Account, Int32> signInManager)
        {
            AuthManager = authManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // set the return Uri for the
            // facebook authentication middleware
            CallbackUri = Url.Action("SignInExternalCallback");
        }

        [ActionName("sign-in")]
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ActionName("sign-in")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(LoginViewModel vm, string returnUrl)
        {
            var account = await UserManager.FindAsync(vm.UserName, vm.Password);

            if (account != null)
            {
                var id = await UserManager.CreateIdentityAsync(account, DefaultAuthenticationTypes.ApplicationCookie);
                var cfg = new AuthenticationProperties { IsPersistent = true };

                AuthManager.SignIn(cfg, id);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel vm)
        {
            var account = new Account(vm.Email);

            var result = await UserManager
                .CreateAsync(account, vm.Password);

            var id = account.Id;
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(id);
            var mail = Url.Action("Confirm", new { id, code });

            if (result.Succeeded)
                await UserManager.SendEmailAsync(account.Id, "email confirmation", mail);

            return View();
        }

        [Route("register/confirm")]
        public async Task<ActionResult> Confirm(int id, string code)
        {
            var result = await UserManager
                .ConfirmEmailAsync(userId: id, token: code);

            if (result.Succeeded)
                return View();

            return RedirectToAction("Index", "Home");
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