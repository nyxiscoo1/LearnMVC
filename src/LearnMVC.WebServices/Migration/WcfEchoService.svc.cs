using System.ServiceModel;

namespace LearnMVC.WebServices.Migration
{
    public class WcfEchoService : IWcfEchoService
    {
        private readonly EchoService echoService = new EchoService();

        public EchoResponse Echo(EchoRequest message)
        {
            return echoService.Echo(message);
        }
    }
}
