using System.IO;
using System.Linq;
using LearnMVC.WebServices;
using NUnit.Framework;

namespace LearnMVC.Tests
{
    [TestFixture]
    public class StreamExtensionsTests
    {
        private DirectoryContext ctx;

        [SetUp]
        public void Context()
        {
            ctx = DirectoryContext.CreateTmp();
        }

        [TearDown]
        public void CleanupContext()
        {
            ctx.Dispose();
        }

        [Test]
        public void TestWriting()
        {
            string fileName = "SomeFile.dat";

            byte[] data = Enumerable.Range(100, 10000).Select(x => (byte)(x % byte.MaxValue)).ToArray();
            using (var ms = new MemoryStream(data))
            {
                ms.WriteToFile(fileName);

                CollectionAssert.AreEqual(data, File.ReadAllBytes(fileName));
            }
        }
    }
}
