using System;
using System.Runtime.Serialization;

namespace LearnMVC.WebServices.Migration
{
    [DataContract]
    public class EchoResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public EchoRequest Request { get; set; }

        public EchoResponse()
        {
        }

        public EchoResponse(int id, DateTime time, EchoRequest request)
        {
            Id = id;
            Time = time;
            Request = request;
        }
    }
}