using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace LearnMVC.Tests
{
    public static class SerializeationHelper
    {
        public static string SoapSerialize(this object data)
        {
            using (var writer = new MemoryStream())
            {
                var frmtr = new SoapFormatter();
                frmtr.Serialize(writer, data);
                return Encoding.ASCII.GetString(writer.ToArray());
            }
        }
    }
}