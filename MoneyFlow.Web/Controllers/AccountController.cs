using System;
using System.Web.Mvc;
using MoneyFlow.Web.ViewModels;

namespace MoneyFlow.Web.Controllers
{
    public class AccountController : Controller
    {
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
    }
}