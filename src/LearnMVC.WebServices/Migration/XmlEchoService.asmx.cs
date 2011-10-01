using System.Web.Services;
using System.Web.Services.Protocols;

namespace LearnMVC.WebServices.Migration
{
    [WebService(Namespace = "http://tempuri.org/", Name = "EchoService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
        // [System.Web.Script.Services.ScriptService]
    public class XmlEchoService : System.Web.Services.WebService, IWcfEchoService
    {
        private readonly EchoService echoService = new EchoService();

        [WebMethod(MessageName = "Echo")]
        [SoapDocumentMethod(Action = "http://tempuri.org/Echo")]
        public EchoResponse Echo(EchoRequest message)
        {
            return echoService.Echo(message);
        }
    }
}
