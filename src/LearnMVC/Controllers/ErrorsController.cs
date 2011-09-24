using System.Web.Mvc;

namespace LearnMVC.Controllers
{
    public partial class ErrorsController : Controller
    {
        public virtual ActionResult NotFound(string aspxerrorpath)
        {
            return View((object)aspxerrorpath);
        }

        public virtual ActionResult Forbidden()
        {
            return View();
        }

        public virtual ActionResult General()
        {
            return View();
        }
    }
}
