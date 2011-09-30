using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;

namespace LearnMVC.WebServices
{
    /// <summary>
    /// Summary description for FileConverter
    /// </summary>
    [WebService(Namespace = "http://tempuri.org", Name = "FileConverter")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FileConverter : System.Web.Services.WebService
    {
        [WebMethod]
        public int GetInt(int i)
        {
            return i + 1;
        }

        [WebMethod]
        public ComplexResponse ComplexMethod(ComplexRequest request)
        {
            if (request == null)
                throw new ArgumentException();

            return new ComplexResponse(request.Id, DateTime.Now.ToString());
        }

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

                string directoryPath = Server.MapPath("~/ForConvertion/");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

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
               
    [DataContract]
    public class ComplexResponse
    {
        public int Id { get; private set; }
        public string Data { get; private set; }

        public ComplexResponse()
        {
        }

        public ComplexResponse(int id, string data)
        {
            Id = id;
            Data = data;
        }
    }

    [DataContract]
    public class ComplexRequest
    {
        public int Id { get; set; }

        public List<ChildItem> Items { get; set; }


    }

    [DataContract]
    public class ChildItem
    {
        public int Id { get; set; }
    }
}
