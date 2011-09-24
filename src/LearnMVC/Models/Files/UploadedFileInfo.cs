namespace LearnMVC.Models.Files
{
    public class UploadedFileInfo
    {
        public string Name { get; private set; }
        public long Length { get; private set; }

        public UploadedFileInfo(string name, long length)
        {
            Name = name;
            Length = length;
        }
    }
}