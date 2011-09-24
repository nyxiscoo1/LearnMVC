using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC.Models.Files;
using MvcContrib.Pagination;

namespace LearnMVC.Controllers
{
    public partial class FilesController : Controller
    {
        public static readonly string UploadFilesFolder = "~/App_Data/uploads";
        public static readonly int DefaultPageSize = 20;

        [HttpGet]
        public virtual ActionResult Index(int? page, int? pageSize)
        {
            var r = new List<UploadFilesResult>();

            string dir = Server.MapPath(UploadFilesFolder);

            if (Directory.Exists(dir))
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    var fi = new FileInfo(file);
                    r.Add(new UploadFilesResult(fi.Name, fi.Length));
                }
            }

            int ps = pageSize ?? DefaultPageSize;
            ps = ps < 0 || ps > DefaultPageSize ? DefaultPageSize : ps;

            return View(r.AsPagination(page ?? 1, ps));
        }

        [HttpGet]
        public virtual ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            var submitedFiles = files.ToArray();

            if (files == null || submitedFiles.Length == 0)
                return View();

            foreach (var file in submitedFiles)
            {
                if (file == null || file.ContentLength == 0)
                    continue;

                string fileName = Path.GetFileName(file.FileName);

                if (string.IsNullOrEmpty(fileName))
                    continue;

                string dir = Server.MapPath(UploadFilesFolder);
                string path = Path.Combine(dir, fileName);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                file.SaveAs(path);
            }

            return RedirectToAction(MVC.Files.Index());
        }

        [HttpGet]
        public virtual ActionResult Get(string fileName)
        {
            string dir = Server.MapPath(UploadFilesFolder);
            string filePath = Path.Combine(dir, fileName);

            if (System.IO.File.Exists(filePath))
            {
                return File(filePath, "application/octet-stream", fileName);
            }

            return RedirectToAction(MVC.Errors.NotFound(fileName));
        }
    }
}
