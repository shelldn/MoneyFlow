using System.Web.Mvc;

namespace MoneyFlow.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}