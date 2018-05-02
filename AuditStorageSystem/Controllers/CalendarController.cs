using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AuditStorageSystem.Framework;
using AuditStorageSystem.Models;

namespace AuditStorageSystem.Controllers
{
    public class CalendarController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Calendar
        public ActionResult Index(string[] selectedServiceTypes, string allTypes = "false", int year = Constants.DefaultYear)
        {
            ViewBag.Year = year;
            bool allFlags;
            bool.TryParse(allTypes, out allFlags);
            
            var audits =
                db.Audits.Where( item =>
                        (item.LastAuditDate != null && item.LastAuditDate.Value.Year == year) ||
                        item.PlannedDate.Year == year).Include(a => a.Project);

            var resultCheckedServiceTypes = HelperForProject.GetResultCheckedServiceTypes(selectedServiceTypes, allFlags);
            
            ViewBag.CheckedServiceTypes = resultCheckedServiceTypes;

            if (resultCheckedServiceTypes.Any())
            {
                //var selectedTypes = projectTypesForView.Where(item => item.Value.Equals(Constants.Checked)).Select(item => item.Key.ParseAsEnum<ServiceTypes>()).ToList();
                audits = audits.Where((item) => resultCheckedServiceTypes.Contains(item.ServiceType));
            }
            else
                audits = audits.Where(item => item.AuditId == 0);

            return View(audits);
        }

        // GET: Calendar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var audit = db.Audits.Find(id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            return View(audit);
        }

        // GET: Calendar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var audit = db.Audits.Find(id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", audit.ProjectId);
            return View(audit);
        }

        // POST: Calendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuditId,ProjectId,ServiceType,PlannedDate,Status,Auditors,Result")] Audit audit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(audit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", audit.ProjectId);
            return View(audit);
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
