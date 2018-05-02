using System;
using System.Collections.Generic;
using System.Linq;
using AuditStorageSystem.Framework;

namespace AuditStorageSystem.Models
{
    public class ProjectView
    {
        public Project Project { get; set; }
        public List<DateTime> PlannedAudits { get; set; }
        public string PlannedAuditsValue => GetPlannedAudits();
        public DateTime? LastAudit { get; set; }
        public string LastAuditValue => GetLastAudit();

        public ProjectView()
        {
            PlannedAudits = new List<DateTime>();
        }

        public string GetPlannedAudits()
        {
            return PlannedAudits.Count > 0
                ? PlannedAudits.Select(date => date.ToString(Constants.DateFormat)).Aggregate((a, b) => a + Environment.NewLine + b)
                : Constants.NA;
        }
        public string GetLastAudit()
        {
            return LastAudit.HasValue
                ? LastAudit.Value.ToString(Constants.DateFormat)
                : Constants.NA;
        }
    }
}