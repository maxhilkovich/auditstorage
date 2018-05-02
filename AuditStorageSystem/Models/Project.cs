using System.Data.Entity;
using AuditStorageSystem.Framework.Enum;
using MySql.Data.MySqlClient;

namespace AuditStorageSystem.Models
{
    [DbConfigurationType(typeof(MySqlConfiguration))]
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public ProjectStatuses Status { get; set; }
        public string UnitCoordinator { get; set; }
        public string UnitManager { get; set; }
        public string ProjectCoordinator { get; set; }
        public string ProjectManager { get; set; }
        public string JiraKey { get; set; }
    }
}