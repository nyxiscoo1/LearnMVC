using System;

namespace LearnMVC.WebServices.Migration
{
    public class EchoService
    {
        public EchoResponse Echo(EchoRequest message)
        {
            return new EchoResponse(message.Id + 1, DateTime.Now, message);
        }
    }
}