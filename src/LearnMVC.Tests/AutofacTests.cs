using System.Linq;
using System.Web.Mvc;
using Autofac;
using NUnit.Framework;

namespace LearnMVC.Tests
{
    [TestFixture]
    public class AutofacTests
    {
        private IContainer container;

        [SetUp]
        public void Context()
        {
            container = AutofacBootstrapper.Configure();
        }

        [TearDown]
        public void CleanupContext()
        {
            if (container != null)
                container.Dispose();
        }

        [Test]
        public void TestControllers()
        {
            var controllers = typeof(MvcApplication).Assembly.GetExportedTypes().Where(x => x.IsAssignableTo<Controller>()).ToList();
            foreach (var controller in controllers)
            {
                Assert.IsTrue(container.IsRegistered(controller), "Controller {0} is not registered", controller.FullName);
                container.Resolve(controller);
            }
        }
    }
}
