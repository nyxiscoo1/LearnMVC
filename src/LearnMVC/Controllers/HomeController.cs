using System.Web.Mvc;

namespace LearnMVC.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            ViewBag.Message = "Welcome to LearnMVC!";

            return View();
        }

        public virtual ActionResult About()
        {
            return View();
        }
    }
}
