using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;
using NUnit.Framework;

namespace LearnMVC.Tests
{
    [TestFixture, Explicit]
    public class FileStorageTests
    {
        [Test]
        public void TestFileUploading()
        {
            WriteFileToService(@"C:\nyx.ddb");
            WriteFileToService(@"D:\Projects\Python\Tetris\dist\bz2.pyd");
            WriteFileToService(@"D:\Projects\direct3d.dll");
        }

        private void WriteFileToService(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            Assert.IsNotNull(fileName);

            var uri = "http://localhost:54147/WebServices/FileStorage.svc/File/" + fileName;            
            //var uri = "http://localhost:8000/Service/File/" + fileName;

            var req = WebRequest.Create(uri) as HttpWebRequest;
            if (req != null)
            {
                req.Method = "POST";
                req.ContentType = "text/plain";

                using (var fs = File.OpenRead(filePath))
                {
                    var reqStream = req.GetRequestStream();

                    fs.CopyTo(reqStream, 4000);

                    reqStream.Close();
                }
                var resp = (HttpWebResponse)req.GetResponse();
                Trace.WriteLine("StatusCode " + resp.StatusCode);
            }
        }

        [Test]
        public void TestGetImage()
        {
            var uri = "http://localhost:54147/WebServices/FileStorage.svc/image?text=someText";
            var req = WebRequest.Create(uri) as HttpWebRequest;
            if (req != null)
            {
                //req.SendChunked = true;
                //req.TransferEncoding = "Mtom";
                
                req.Method = "GET";
                var resp = (HttpWebResponse)req.GetResponse();
                var r = new XmlTextReader(resp.GetResponseStream());
                if (r.Read())
                {
                    Console.WriteLine(r.ReadString());
                }
            }
        }
    }
}
