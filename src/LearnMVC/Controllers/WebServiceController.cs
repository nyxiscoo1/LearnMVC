using System;
using System.Web.Mvc;
using LearnMVC.EchoService;

namespace LearnMVC.Controllers
{
    public partial class WebServiceController : Controller
    {
        public static readonly string WebServiceCallResultKeyName = "i";

        public virtual ActionResult Index()
        {
            return View(TempData[WebServiceCallResultKeyName] ?? 0);
        }

        [HttpPost]
        public virtual ActionResult ServiceRequest(int i)
        {
            var message = new EchoRequest();
            message.Id = i;
            message.Time = DateTime.Now;

            TempData[WebServiceCallResultKeyName] = new EchoServiceSoapClient().Echo(message).Id;
            return Redirect(MVC.WebService.ActionNames.Index);
        }
    }
}
