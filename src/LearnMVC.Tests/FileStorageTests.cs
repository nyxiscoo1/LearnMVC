using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Http;
using NUnit.Framework;
using RestSharp;

namespace LearnMVC.Tests
{
    [TestFixture, Explicit]
    public class FileStorageTests
    {
        public static readonly string ServiceUri = "http://localhost:54147/WebServices/FileStorage.svc/";

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

            var uri = ServiceUri + "File/" + Uri.EscapeUriString(fileName);

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
        public void TestFileUploading_using_restsharp()
        {
            UploadFileToServiceUsingPostSharp(@"C:\nyx.ddb");
            ReadFileUsingRestSharp(@"C:\nyx.ddb");
            UploadFileToServiceUsingPostSharp(@"D:\Projects\Python\Tetris\dist\bz2.pyd");
            ReadFileUsingRestSharp(@"D:\Projects\Python\Tetris\dist\bz2.pyd");
            UploadFileToServiceUsingPostSharp(@"D:\Projects\direct3d.dll");
            ReadFileUsingRestSharp(@"D:\Projects\direct3d.dll");
        }

        private void UploadFileToServiceUsingPostSharp(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            Assert.IsNotNull(fileName);

            var restClient = new RestClient(ServiceUri);

            var request = new RestRequest("File/{fileName}", Method.POST);

            request.AddParameter("fileName", fileName, ParameterType.UrlSegment);

            //request.AddFile(fileName, filePath);

            //request.AddFile(fileName, x => File.OpenRead(filePath).CopyTo(x), fileName);
            request.AddFile(fileName, File.ReadAllBytes(filePath), fileName, "text/plain");
            var response = restClient.Execute(request);

            Assert.AreEqual(ResponseStatus.Completed, response.ResponseStatus);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private void ReadFileUsingRestSharp(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            Assert.IsNotNull(fileName);

            var restClient = new RestClient(ServiceUri);
            var request = new RestRequest("File/{fileName}", Method.GET);

            request.AddParameter("fileName", fileName, ParameterType.UrlSegment);
            byte[] downloaded = restClient.DownloadData(request);

            //Assert.AreEqual(ResponseStatus.Completed, response.ResponseStatus);
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            CollectionAssert.AreEqual(File.ReadAllBytes(filePath), downloaded);
        }

        [Test]
        public void TestUploadFile_using_http_client()
        {
            UploadFileToServiceUsingHttpClient(@"C:\nyx.ddb");
            ReadFileUsingHttpClient(@"C:\nyx.ddb");
            UploadFileToServiceUsingHttpClient(@"D:\Projects\Python\Tetris\dist\bz2.pyd");
            ReadFileUsingHttpClient(@"D:\Projects\Python\Tetris\dist\bz2.pyd");
            UploadFileToServiceUsingHttpClient(@"D:\Projects\direct3d.dll");
            ReadFileUsingHttpClient(@"D:\Projects\direct3d.dll");
        }

        private void UploadFileToServiceUsingHttpClient(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            Assert.IsNotNull(fileName);

            var client = new HttpClient(ServiceUri);

            var response = client.Post("File/" + Uri.EscapeUriString(fileName), "application/octet-stream",
                        HttpContent.Create(x => File.OpenRead(filePath).CopyTo(x)));


            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private void ReadFileUsingHttpClient(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            Assert.IsNotNull(fileName);

            var client = new HttpClient(ServiceUri);

            var response = client.Get("File/" + Uri.EscapeUriString(fileName));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            byte[] downloaded = response.Content.ReadAsByteArray();

            CollectionAssert.AreEqual(File.ReadAllBytes(filePath), downloaded);
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
