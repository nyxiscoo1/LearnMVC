namespace LearnMVC.Models.Files
{
    public class UploadFilesResult
    {
        public string Name { get; private set; }
        public long Length { get; private set; }

        public UploadFilesResult(string name, long length)
        {
            Name = name;
            Length = length;
        }
    }
}