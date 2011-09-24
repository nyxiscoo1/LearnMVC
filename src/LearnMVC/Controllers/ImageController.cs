using System.Web.Mvc;
using LearnMVC.Infrastructure;

namespace LearnMVC.Controllers
{
    public partial class ImageController : Controller
    {
        private readonly IFileSystem fileSystem;
        public static readonly string ImageFolder = "~/Content/Images/";
        public static readonly string DefaultImageName = "default.jpg";

        public ImageController(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public virtual ActionResult Index(string imageName)
        {
            string requestedImageName = fileSystem.MapPath(ImageFolder + imageName);
            if (!fileSystem.FileExists(requestedImageName))
                requestedImageName = fileSystem.MapPath(ImageFolder + DefaultImageName);

            return File(fileSystem.OpenRead(requestedImageName), "image");
        }
    }
}
