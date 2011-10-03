using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LearnMVC.WebServices
{
    [ServiceContract]
    public interface IFileStorage
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/File")]
        FileDescription[] GetAllFiles();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/File?filter={filter}")]
        FileDescription[] GetFilteredFiles(string filter);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "File/{fileName}")]
        void UploadFile(string fileName, Stream fileStream);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "File/{fileName}")]
        void ReUploadFile(string fileName, Stream fileStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/File/{fileName}")]
        Stream GetFile(string fileName);

        [WebGet(UriTemplate = "image?text={text}")]
        [OperationContract]
        Stream GetImage(string text);
    }
}
