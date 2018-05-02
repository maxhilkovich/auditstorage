using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuditStorageSystem.Framework.Authorization;
using Microsoft.Ajax.Utilities;
using AuditStorageSystem.Framework.Enum;
using AuditStorageSystem.Models;
using WebGrease.Css.Extensions;
using Roles = AuditStorageSystem.Framework.Enum.Roles;

namespace AuditStorageSystem.Controllers
{
    public class AuditsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Audits
        public ActionResult Index(int? id)
        {
            var audits = from s in db.Audits select s;
            if (id != null)
            {
                audits = audits.Where(audit => audit.ProjectId == id);
            }

            ViewBag.IsCreateEnable = AccessUtility.IsCreateEnable(db, User.Identity.Name);
            return View(audits.Include(a => a.Project).ToList());
        }

        // GET: Audits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var audit = db.Audits.Include(s => s.Project).Include(s => s.Files).SingleOrDefault(s => s.AuditId == id);
            if (audit == null)
            {
                return HttpNotFound();
            }

            ViewBag.IsAddResultEnable = AccessUtility.IsAddResultEnable(db, User.Identity.Name) &&
                                        (audit.Status == AuditStatuses.Planned || audit.Status == AuditStatuses.InProgress);
            ViewBag.IsEditEnable = AccessUtility.IsEditEnable(db, User.Identity.Name);
            ViewBag.IsDeleteEnable = AccessUtility.IsDeleteEnable(db, User.Identity.Name);
            return View(audit);
        }

        // GET: Audits/Create
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin))]
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View();
        }

        // POST: Audits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin))]
        public ActionResult Create([Bind(Include = "AuditId,ProjectId,ServiceType,PlannedDate,Status,Auditors,Result")] Audit createdAudit, IEnumerable<HttpPostedFileBase> uploads)
        {
            if (ModelState.IsValid)
            {
                if (createdAudit.Status.Equals(AuditStatuses.Closed) || createdAudit.Status.Equals(AuditStatuses.Passed))
                {
                    createdAudit.LastAuditDate = DateTime.Today;
                }

                uploads?.ForEach(upload =>
                {
                    if (upload == null || upload.ContentLength == 0) return;
                    var file = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        file.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    createdAudit.Files.Add(file);
                });

                db.Audits.Add(createdAudit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", createdAudit.ProjectId);
            return View(createdAudit);
        }

        // GET: Audits/Edit/5
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Audit audit = db.Audits.Find(id);
            var audit = db.Audits.Include(s => s.Files).SingleOrDefault(s => s.AuditId == id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", audit.ProjectId);
            return View(audit);
        }

        // POST: Audits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) +","+ nameof(Roles.Admin))]
        public ActionResult Edit([Bind(Include = "AuditId,ProjectId,ServiceType,PlannedDate,Status,Auditors,Result")] Audit updatedAudit, IEnumerable<HttpPostedFileBase> uploads)
        {
            if (ModelState.IsValid)
            {
                var oldAudit = db.Audits.Include(s => s.Files).First(item => item.AuditId == updatedAudit.AuditId);

                if ((oldAudit.Status.Equals(AuditStatuses.Planned) || oldAudit.Status.Equals(AuditStatuses.InProgress))
                    &&(updatedAudit.Status.Equals(AuditStatuses.Closed) || updatedAudit.Status.Equals(AuditStatuses.Passed)))
                {
                    updatedAudit.LastAuditDate = DateTime.Today;
                }
                else
                {
                    updatedAudit.LastAuditDate = oldAudit.LastAuditDate;
                }
                
                uploads?.ForEach(upload =>
                {
                    if (upload == null || upload.ContentLength == 0) return;
                    var file = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        ContentType = upload.ContentType,
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        file.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    file.AuditId = updatedAudit.AuditId;
                    db.Files.Add(file);
                });

                db.Audits.AddOrUpdate(updatedAudit);
                //db.Entry(audit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", updatedAudit.ProjectId);
            return View(updatedAudit);
        }

        // GET: Audits/Edit/5
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin) + "," + nameof(Roles.Auditor))]
        public ActionResult AddResult(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Audit audit = db.Audits.Find(id);
            var audit = db.Audits.Include(s => s.Files).SingleOrDefault(s => s.AuditId == id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", audit.ProjectId);
            return View(audit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin) + "," + nameof(Roles.Admin) + "," + nameof(Roles.Auditor))]
        public ActionResult AddResult([Bind(Include = "AuditId,Status,Result")] Audit updatedAudit, IEnumerable<HttpPostedFileBase> uploads)
        {
            if (ModelState.IsValid)
            {
                var oldAudit = db.Audits.Include(s => s.Files).First(item => item.AuditId == updatedAudit.AuditId);

                if (oldAudit.Status.Equals(AuditStatuses.Closed) || oldAudit.Status.Equals(AuditStatuses.Passed))
                {
                    return RedirectToAction("Index");
                }

                updatedAudit.LastAuditDate = DateTime.Today;
                updatedAudit.Status = AuditStatuses.Passed;
                updatedAudit.PlannedDate = oldAudit.PlannedDate;
                updatedAudit.ProjectId = oldAudit.ProjectId;
                updatedAudit.Auditors = oldAudit.Auditors;
                updatedAudit.Goal = oldAudit.Goal;
                updatedAudit.ServiceType = oldAudit.ServiceType;
                
                uploads?.ForEach(upload =>
                {
                    if (upload == null || upload.ContentLength == 0) return;
                    var file = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        ContentType = upload.ContentType,
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        file.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    file.AuditId = updatedAudit.AuditId;
                    db.Files.Add(file);
                });


                db.Audits.AddOrUpdate(updatedAudit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", updatedAudit.ProjectId);
            return View(updatedAudit);
        }


        // GET: Audits/Delete/5
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin))]
        public ActionResult Delete(int? id)
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

        // POST: Audits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Roles = nameof(Roles.SuperAdmin))]
        public ActionResult DeleteConfirmed(int id)
        {
            var audit = db.Audits.Find(id);
            db.Audits.Remove(audit);
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
