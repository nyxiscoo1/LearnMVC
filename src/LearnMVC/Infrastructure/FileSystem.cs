using System;
using System.IO;
using System.Web;

namespace LearnMVC.Infrastructure
{
    public class FileSystem : IFileSystem
    {
        public string MapPath(string path)
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("HttpContext.Current is null");

            return HttpContext.Current.Server.MapPath(path);
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public FileStream OpenRead(string filePath)
        {
            return File.OpenRead(filePath);
        }
    }

    public interface IFileSystem
    {
        string MapPath(string path);
        bool FileExists(string filePath);
        FileStream OpenRead(string filePath);
    }
}