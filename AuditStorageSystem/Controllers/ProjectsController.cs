using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AuditStorageSystem.Framework;
using AuditStorageSystem.Framework.Authorization;
using AuditStorageSystem.Framework.Enum;
using AuditStorageSystem.Models;

namespace AuditStorageSystem.Controllers
{
    public class ProjectsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Projects
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var projects = from s in db.Projects select s;
            
            if (!string.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(s => s.Name.Contains(searchString));
            }

            var audits = from s in db.Audits select s;

            var projectView = HelperForProject.CreateProjectView(projects.ToList(), audits.ToList());

            switch (sortOrder)
            {
                case "name_desc":
                    projectView = projectView.OrderByDescending(s => s.Project.Name);
                    break;
                //case "Date":
                //    projects = projects.OrderBy(s => s.PlannedDate);
                //    break;
                //case "date_desc":
                //    projects = projects.OrderByDescending(s => s.PlannedDate);
                //    break;
                default:
                    projectView = projectView.OrderBy(s => s.Project.Name);
                    break;
            }

            ViewBag.IsCreateEnable = AccessUtility.IsCreateEnable(db, User.Identity.Name);
            return View(projectView.ToList());

        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.IsEditEnable = AccessUtility.IsEditEnable(db, User.Identity.Name);
            ViewBag.IsDeleteEnable = AccessUtility.IsDeleteEnable(db, User.Identity.Name);
            return View(project);
        }

        // GET: Projects/Create
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin))]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin))]
        public ActionResult Create([Bind(Include = "ProjectId,Name,Status,UnitCoordinator,UnitManager,ProjectCoordinator,ProjectManager,JiraKey")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin))]
        public ActionResult Edit([Bind(Include = "ProjectId,Name,Status,UnitCoordinator,UnitManager,ProjectCoordinator,ProjectManager,JiraKey")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin))]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
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
