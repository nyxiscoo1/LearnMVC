using System;
using System.Runtime.Serialization;

namespace LearnMVC.WebServices.Migration
{
    [DataContract(Namespace = "http://tempuri.org/", Name = "EchoRequest")]
    [Serializable]
    public class EchoRequest
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Time { get; set; }
    }
}