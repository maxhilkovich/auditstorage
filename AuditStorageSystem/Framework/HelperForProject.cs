using System;
using System.Collections.Generic;
using System.Linq;
using AuditStorageSystem.Framework.Enum;
using AuditStorageSystem.Framework.Extensions;
using AuditStorageSystem.Models;

namespace AuditStorageSystem.Framework
{
    public class HelperForProject
    {
        public static string GetJiraLink(string key)
        {
            return Constants.JiraUrl + key;
        }

        private static ProjectView CreateProjectView(Project project, List<Audit> audits)
        {
            var auditsForCurrentProject = audits.Where(audit => audit.ProjectId == project.ProjectId).ToList();
            var lastAudit = GetLastAudit(project, auditsForCurrentProject);
            var plannedAudits = GetPlannedAudits(project, auditsForCurrentProject);
            
            return new ProjectView() { Project = project, LastAudit = lastAudit, PlannedAudits = plannedAudits};
        }

        public static DateTime? GetLastAudit(Project project, List<Audit> auditsForCurrentProject)
        {
            return auditsForCurrentProject
                .ToList()
                .Where(audit => audit.Status == AuditStatuses.Passed || audit.Status == AuditStatuses.Closed)
                .Select(audit => audit.LastAuditDate)
                .Min();
        }

        public static List<DateTime> GetPlannedAudits(Project project, List<Audit> auditsForCurrentProject)
        {
            return auditsForCurrentProject
                .ToList()
                .Where(audit =>
                        audit.Status == AuditStatuses.Planned ||
                        audit.Status == AuditStatuses.InProgress)
                .Select(audit => audit.PlannedDate).ToList();
        }

        public static IQueryable<ProjectView> CreateProjectView(List<Project> projects, List<Audit> audits)
        {
            var store = new List<ProjectView>();

            projects.ForEach(project =>
            {
                store.Add(CreateProjectView(project, audits));
            });

            return store.AsQueryable();
        }

        public static Dictionary<string, string> AnalizeProjectTypesForCalendar(List<ServiceTypes> actualTypes, bool allTypes)
        {
            var serviceTypes = new Dictionary<string, string>();

            foreach (ServiceTypes type in System.Enum.GetValues(typeof(ServiceTypes)))
            {
                if ((actualTypes != null && actualTypes.Contains(type)) || allTypes)
                    serviceTypes.Add(type.GetStringMapping(), Constants.Checked);
                else
                    serviceTypes.Add(type.GetStringMapping(), Constants.Unchecked);
            }

            return serviceTypes;
        }

        public static List<ServiceTypes> GetResultCheckedServiceTypes(string[] selectedServiceTypes, bool allTypes)
        {
            var selectedServiceTypesList = selectedServiceTypes?.Select(item => item.ParseAsEnum<ServiceTypes>()).ToList();
            return GetResultCheckedServiceTypes(selectedServiceTypesList, allTypes);
        }

        public static List<ServiceTypes> GetResultCheckedServiceTypes(List<ServiceTypes> actualTypes, bool allTypes)
        {
            if (actualTypes != null && actualTypes.Any())
            {
                allTypes = false;
            }
            return
                System.Enum.GetValues(typeof(ServiceTypes))
                    .Cast<ServiceTypes>()
                    .Where(type => allTypes || (actualTypes != null && actualTypes.Contains(type)))
                    .ToList();
        }
    }
}