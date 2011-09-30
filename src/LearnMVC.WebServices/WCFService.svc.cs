using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace LearnMVC.WebServices
{
    public class WCFService : IWCFService
    {
        public int GetInt(int i)
        {
            return i + 1;
        }

        public ComplexResponse ComplexMethod(ComplexRequest request)
        {
            if (request == null)
                throw new ArgumentException();

            return new ComplexResponse(request.Id, DateTime.Now.ToString());
        }

        public bool UploadFile()
        {                 
            try
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;

                if (files.Count != 1 || files[0].ContentLength <= 1)
                    return false;

                byte[] data = new byte[files[0].ContentLength];
                files[0].InputStream.Read(data, 0, (int)files[0].InputStream.Length);

                string directoryPath = HttpContext.Current.Server.MapPath("~/ForConvertion/");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                string fileName = HttpContext.Current.Server.MapPath("~/ForConvertion/" + Guid.NewGuid().ToString());
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
