using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC.Infrastructure;
using LearnMVC.Models.Files;
using LearnMVC.WebServices;
using MvcContrib.Pagination;

namespace LearnMVC.Controllers
{
    public partial class FilesController : Controller
    {
        public static readonly int DefaultPageSize = 20;

        private readonly FileStorage fileStorage;

        public FilesController()
        {
            fileStorage = new FileStorage();
        }

        [HttpGet]
        public virtual ActionResult Index(int? page, int? pageSize)
        {
            var r = fileStorage.GetFiles();

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

                fileStorage.SaveFile(fileName, file.InputStream);
            }

            return RedirectToAction(MVC.Files.Index());
        }

        [HttpGet]
        public virtual ActionResult Get(string fileName)
        {
            try
            {
                var strm = fileStorage.GetFile(fileName);
                return File(strm, "application/octet-stream", fileName);
            }
            catch (FileNotFoundException)
            {
                return RedirectToAction(MVC.Errors.NotFound(fileName));
            }
        }

        [HttpGet]
        public virtual ActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult QuickSearch(string term)
        {
            if (string.IsNullOrEmpty(term))
                return Json(null, JsonRequestBehavior.AllowGet);

            var names = fileStorage.GetFiles(term + "*")
                .Where(x => x.Name.StartsWith(term)).Select(x => new { label = x.Name }).ToList();

            return Json(names, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult JsonSearch(string q)
        {
            var names = fileStorage.GetFiles(q + "*")
                .Where(x => x.Name.StartsWith(q)).Take(10).Select(x => new { x.Name, x.Length, EditUrl = Url.Action(MVC.Files.Get(x.Name)) }).ToList();

            return Json(names, JsonRequestBehavior.AllowGet);
        }
    }

    public class FileStorage
    {
        public static readonly string UploadFilesFolder = "~/App_Data/uploads";
        public static readonly int BufferSize = 4 * 1024;

        private readonly IFileSystem fs = new FileSystem();

        public IEnumerable<UploadedFileInfo> GetFiles()
        {
            return GetFiles("*.*");
        }

        public IEnumerable<UploadedFileInfo> GetFiles(string filter)
        {
            string dir = fs.MapPath(UploadFilesFolder);

            if (Directory.Exists(dir))
            {
                foreach (var file in Directory.GetFiles(dir, filter, SearchOption.TopDirectoryOnly))
                {
                    var fi = new FileInfo(file);
                    yield return new UploadedFileInfo(fi.Name, fi.Length);
                }
            }
        }

        public void SaveFile(string fileName, Stream inputStream)
        {
            string dir = fs.MapPath(UploadFilesFolder);
            string path = Path.Combine(dir, fileName);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            inputStream.WriteToFile(path);
        }

        public Stream GetFile(string fileName)
        {
            string dir = fs.MapPath(UploadFilesFolder);
            string filePath = Path.Combine(dir, fileName);

            if (!fs.FileExists(filePath))
                throw new FileNotFoundException("File [" + fileName + "] not found", fileName);

            return fs.OpenRead(filePath);

        }
    }
}
