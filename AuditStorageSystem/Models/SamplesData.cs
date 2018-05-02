using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using AuditStorageSystem.Framework.Enum;
using AuditStorageSystem.Models.Authorization;

namespace AuditStorageSystem.Models
{
    public static class SamplesData
    {
        private static readonly string NewLine = Environment.NewLine;

        public static List<Role> GetDefaultRoles()
        {
            return new List<Role>
            {
                new Role
                {
                    Name = nameof(Roles.SuperAdmin),
                    Id = 1,
                    Description = "Full Access"
                },
                new Role
                {
                    Name = nameof(Roles.Admin),
                    Id = 2,
                    Description = "Access without deletion operations"
                },
                new Role
                {
                    Name = nameof(Roles.Auditor),
                    Id = 3,
                    Description = "Only view information and add audit result"
                },
                new Role
                {
                    Name = nameof(Roles.Viewer),
                    Id = 4,
                    Description = "Only view information"
                }
            };
        }

        public static List<User> GetUserInRole()
        {
            var domain = "company";
            return new List<User>
            {
                new User
                {
                    UserName = "m.hilkovich",
                    Domain = domain,
                    Id = 1,
                    RoleId = 1
                },
                new User
                {
                    UserName = "user1",
                    Domain = domain,
                    Id = 2,
                    RoleId = 2
                },
                new User
                {
                    UserName = "user2",
                    Domain = domain,
                    Id = 3,
                    RoleId = 2
                },
                new User
                {
                    UserName = "user3",
                    Domain = domain,
                    Id = 4,
                    RoleId = 4
                },
                new User
                {
                    UserName = "user4",
                    Domain = domain,
                    Id = 5,
                    RoleId = 3
                }
            };
        }

        public static List<File> GetFilesSample()
        {
            var filesSample = new List<File>();
            return filesSample;
        }

        private static File GetFileModel(int fileId, int auditId, string fileName)
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "Content" + Path.DirectorySeparatorChar +
                           "reportResult" + Path.DirectorySeparatorChar + fileName;
            var fileContent = System.IO.File.ReadAllBytes(filePath);

            return new File()
            {
                FileId = fileId,
                AuditId = auditId,
                FileName = Path.GetFileName(fileName),
                Content = fileContent,
                ContentType = MimeMapping.GetMimeMapping(fileName)
            };
        }

        public static List<Project> GetAnyProjects()
        {
            var projectsSample = new List<Project>
            {
                new Project()
                {
                    Name = "Project1",
                    ProjectId = 100,
                    Status = ProjectStatuses.Active,
                    ProjectCoordinator = "PC1",
                    ProjectManager = "PM1",
                    UnitCoordinator = "UC1",
                    UnitManager = "UM1",
                    JiraKey = "TESTPROJECT1"
                },
                new Project()
                {
                    Name = "Project2",
                    ProjectId = 102,
                    Status = ProjectStatuses.Active,
                    ProjectCoordinator = "PC2",
                    ProjectManager = "PM2",
                    UnitCoordinator = "UC2",
                    UnitManager = "UM2",
                    JiraKey = "TESTPROJECT2"
                },
                new Project()
                {
                    Name = "Project3",
                    ProjectId = 103,
                    Status = ProjectStatuses.Active,
                    ProjectCoordinator = "PC3",
                    ProjectManager = "PM3",
                    UnitCoordinator = "UC3",
                    UnitManager = "UM3",
                    JiraKey = "TESTPROJECT3"
                },
                new Project()
                {
                    Name = "Project4",
                    ProjectId = 104,
                    Status = ProjectStatuses.Active,
                    ProjectCoordinator = "PC4",
                    ProjectManager = "PM4",
                    UnitCoordinator = "UC4",
                    UnitManager = "UM4",
                    JiraKey = "TESTPROJECT4"
                },
                new Project()
                {
                    Name = "Pro44",
                    ProjectId = 105,
                    Status = ProjectStatuses.Closed,
                    ProjectCoordinator = "PC5",
                    ProjectManager = "PM5",
                    UnitCoordinator = "UC5",
                    UnitManager = "UM5",
                    JiraKey = "TESTPROJECT5"
                },
                new Project()
                {
                    Name = "Project6",
                    ProjectId = 106,
                    Status = ProjectStatuses.Active,
                    ProjectCoordinator = "PC6",
                    ProjectManager = "PM6",
                    UnitCoordinator = "UC6",
                    UnitManager = "UM6",
                    JiraKey = "TESTPROJECT6"
                }
            };
            
            return projectsSample;
        }

        public static List<Audit> GetAnyAudits()
        {
            var auditSample = new List<Audit>
            {
                new Audit()
                {
                    AuditId = 100,
                    ProjectId = 100,
                    ServiceType = ServiceTypes.Auto,
                    Auditors = "Ivan Ivanov, Peter Petrovich",
                    PlannedDate = DateTime.Now.Date.AddDays(5),
                    LastAuditDate = DateTime.Now.Date.AddDays(5),
                    Result = "Result1",
                    Status = AuditStatuses.Closed
                },
                new Audit()
                {
                    AuditId = 102,
                    ProjectId = 102,
                    ServiceType = ServiceTypes.Mob,
                    Auditors = "Anton Antonovich, Anton Antonovich",
                    PlannedDate = DateTime.Now.Date.AddDays(12),
                    LastAuditDate = DateTime.Now.Date.AddDays(10),
                    Result = "Result2",
                    Status = AuditStatuses.Closed
                },
                new Audit()
                {
                    AuditId = 103,
                    ProjectId = 104,
                    ServiceType = ServiceTypes.Perf,
                    Auditors = "Peter Petrovich, Ivan Ivanov",
                    PlannedDate = DateTime.Now.Date.AddDays(540),
                    Result = "Result3",
                    Status = AuditStatuses.Planned
                },
                new Audit()
                {
                    AuditId = 104,
                    ProjectId = 104,
                    ServiceType = ServiceTypes.Sec,
                    Auditors = "Ivan Ivanov, Anton Antonovich",
                    PlannedDate = DateTime.Now.Date.AddDays(30),
                    LastAuditDate = DateTime.Now.Date.AddDays(-20),
                    Result = "Result4",
                    Status = AuditStatuses.Passed
                },
                new Audit()
                {
                    AuditId = 105,
                    ProjectId = 105,
                    ServiceType = ServiceTypes.Sec,
                    Auditors = "Ivan Ivanov, Anton Antonovich",
                    PlannedDate = DateTime.Now.Date.AddDays(20),
                    LastAuditDate = DateTime.Now.Date.AddDays(24),
                    Result = "Result5",
                    Status = AuditStatuses.Passed
                },
                new Audit()
                {
                    AuditId = 106,
                    ProjectId = 104,
                    ServiceType = ServiceTypes.Sec,
                    Auditors = "Ivan Ivanov, Anton Antonovich",
                    PlannedDate = DateTime.Now.Date.AddDays(60),
                    Result = "Result6",
                    Status = AuditStatuses.Planned
                },
                new Audit()
                {
                    AuditId = 107,
                    ProjectId = 104,
                    ServiceType = ServiceTypes.Auto,
                    Auditors = "Ivan Ivanov, Anton Antonovich",
                    PlannedDate = DateTime.Now.Date.AddDays(55),
                    Result = "Result7",
                    Status = AuditStatuses.Planned
                },
                new Audit()
                {
                    AuditId = 108,
                    ProjectId = 104,
                    ServiceType = ServiceTypes.Perf,
                    Auditors = "Peter Petrovich, Ivan Ivanov",
                    PlannedDate = DateTime.Now.Date.AddDays(-40),
                    Result = "Result8",
                    Status = AuditStatuses.InProgress
                },
                new Audit()
                {
                    AuditId = 109,
                    ProjectId = 104,
                    ServiceType = ServiceTypes.Telco,
                    Auditors = "Ivan Ivanov, Anton Antonovich",
                    PlannedDate = DateTime.Now.Date.AddDays(-60),
                    Result = "Result9",
                    Status = AuditStatuses.Planned
                },
                new Audit()
                {
                    AuditId = 110,
                    ProjectId = 104,
                    ServiceType = ServiceTypes.Telco,
                    Auditors = "Ivan Ivanov, Anton Antonovich",
                    PlannedDate = DateTime.Now.Date.AddYears(-1),
                    LastAuditDate = DateTime.Now.Date.AddMonths(-11),
                    Result = "Result9",
                    Status = AuditStatuses.Passed
                }

            };

            return auditSample;
        }
    }
}