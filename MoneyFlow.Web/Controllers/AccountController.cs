using System.Web.Mvc;

namespace MoneyFlow.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult SignIn(string returnUrl)
        {
            return View();
        }
    }
}