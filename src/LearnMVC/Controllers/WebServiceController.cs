using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnMVC.Controllers
{
    public partial class WebServiceController : Controller
    {
        public virtual ActionResult Index()
        {
            return View(TempData["i"] ?? 0);
        }

        [HttpPost]
        public virtual ActionResult ServiceRequest(int i)
        {
            TempData["i"] = new ServiceReference1.FileConverterClient().GetInt(i);
            return Redirect(MVC.WebService.ActionNames.Index);
        }
    }
}
