using System.Web.Mvc;
using AuditStorageSystem.Models;

namespace AuditStorageSystem.Controllers
{
    public class FileController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: File
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }

        public FileResult Download(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            var response = new FileContentResult(fileToRetrieve.Content, fileToRetrieve.ContentType);
            response.FileDownloadName = fileToRetrieve.FileName;
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}