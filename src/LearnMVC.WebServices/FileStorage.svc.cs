using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Microsoft.ServiceModel.Web;

namespace LearnMVC.WebServices
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FileStorage : IFileStorage
    {
        public static readonly string FileDirectory = "C:\\UploadedFiles";

        public FileDescription[] GetAllFiles(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "*.*";

            return GetFiles(filter).ToArray();
        }

        private IEnumerable<FileDescription> GetFiles(string filter)
        {
            foreach (var file in Directory.GetFiles(FileDirectory, filter, SearchOption.TopDirectoryOnly))
            {
                var fi = new FileInfo(file);
                yield return new FileDescription(fi.Name, fi.Length);
            }
        }

        public void UploadFile(string fileName, Stream fileStream)
        {
            
            if (string.IsNullOrEmpty(fileName))
                throw new WebProtocolException(HttpStatusCode.BadRequest, "file name must be specified", null);

            if (fileStream == null)
                throw new WebProtocolException(HttpStatusCode.BadRequest, "file must be specified", null);

            fileStream.WriteToFile(Path.Combine(FileDirectory, fileName));
        }

        public Stream GetFile(string fileName)
        {
            string path = Path.Combine(FileDirectory, fileName);

            if (!File.Exists(path))
                throw new WebProtocolException(HttpStatusCode.NotFound, "File not found", null);

            return File.OpenRead(path);
        }

        public Stream GetImage(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new WebProtocolException(HttpStatusCode.BadRequest, "text must be specified", null);
            }
            Bitmap theBitmap = GenerateImage(text);
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return new AdapterStream((stream) => theBitmap.Save(stream, ImageFormat.Jpeg));
        }

        Bitmap GenerateImage(string text)
        {
            Bitmap bitmap = new Bitmap(468, 60);
            Graphics g = Graphics.FromImage(bitmap);
            Brush brush = new SolidBrush(Color.Indigo);
            g.FillRectangle(brush, 0, 0, 468, 60);
            brush = new SolidBrush(Color.WhiteSmoke);
            g.DrawString(text, new Font("Consolas", 13), brush, new PointF(5, 5));
            return bitmap;
        }
    }
}
