using System.Runtime.Serialization;

namespace LearnMVC.WebServices
{
    [DataContract]
    public class FileDescription
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long Lenght { get; set; }

        public FileDescription()
        {
        }

        public FileDescription(string name, long lenght)
        {
            Name = name;
            Lenght = lenght;
        }
    }
}