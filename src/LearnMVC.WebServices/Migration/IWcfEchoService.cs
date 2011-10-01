using System.ServiceModel;

namespace LearnMVC.WebServices.Migration
{
    [ServiceContract(Namespace = "http://tempuri.org/", Name = "EchoService")]
    public interface IWcfEchoService
    {
        [OperationContract(Action = "http://tempuri.org/Echo", ReplyAction = "http://tempuri.org/Echo")]
        EchoResponse Echo(EchoRequest message);
    }
}
