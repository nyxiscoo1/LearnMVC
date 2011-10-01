using System.IO;

namespace LearnMVC.WebServices
{
    public static class StreamExtensions
    {
        public static readonly int BufferSize = 1024 * 4;

        public static void WriteToFile(this Stream stream, string filePath)
        {
            using (var sw = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                stream.CopyTo(sw);
            }
        }
    }
}