using System;
using System.IO;
using System.Web;
using System.Web.Services;

namespace LearnMVC.WebServices
{
    /// <summary>
    /// Summary description for FileConverter
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FileConverter : System.Web.Services.WebService
    {
        public FileConverter()
        {

        }

        //[WebMethod]
        //public Guid SaveFile(byte[] data)
        //{
        //    return "Hello World";
        //}

        [WebMethod]
        public bool UploadFile()
        {
            try
            {
                HttpFileCollection files = Context.Request.Files;

                if (files.Count != 1 || files[0].ContentLength <= 1)
                    return false;

                byte[] data = new byte[files[0].ContentLength];
                files[0].InputStream.Read(data, 0, (int)files[0].InputStream.Length);

                string fileName = Server.MapPath("~/ForConvertion/" + Guid.NewGuid().ToString());
                File.WriteAllBytes(fileName, data);

                return true;
            }
            catch (Exception ex1)
            {
                throw new Exception("Problem uploading file: " + ex1.Message);
            }
        }
    }
}
