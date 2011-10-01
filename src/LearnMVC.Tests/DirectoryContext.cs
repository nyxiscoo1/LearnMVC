using System;
using System.IO;
using LearnMVC.Infrastructure;

namespace LearnMVC.Tests
{
    public class DirectoryContext : IDisposable
    {
        private readonly string cwd;

        public string CurrentDir { get; private set; }

        public static DirectoryContext CreateTmp()
        {
            return new DirectoryContext(FileSystem.CreateTempDirectory());
        }

        public static DirectoryContext Create(string directory)
        {
            return new DirectoryContext(directory);
        }

        private DirectoryContext(string directory)
        {
            this.CurrentDir = directory;
            cwd = Environment.CurrentDirectory;
            Environment.CurrentDirectory = this.CurrentDir;
        }

        public void Dispose()
        {
            Environment.CurrentDirectory = cwd;
            Directory.Delete(this.CurrentDir, true);
        }
    }
}