using System;
using System.Collections.Generic;
using System.Data.Entity;
using GridMvc.DataAnnotations;
using AuditStorageSystem.Framework.Enum;
using MySql.Data.MySqlClient;

namespace AuditStorageSystem.Models
{
    [DbConfigurationType(typeof(MySqlConfiguration))]
    public class Audit
    {
        public int AuditId { get; set; }
        public int ProjectId { get; set; }
        public ServiceTypes ServiceType { get; set; }
        [GridColumn(Format = "{0:dd.MM.yyyy}")]
        public DateTime? LastAuditDate { get; set; }
        [GridColumn(Format = "{0:dd.MM.yyyy}")]
        public DateTime PlannedDate { get; set; }
        public AuditStatuses Status { get; set; }
        public string Auditors { get; set; }
        public string Result { get; set; }
        public string Goal { get; set; }
        public string Comments { get; set; }
        public Project Project { get; set; }

        public ICollection<File> Files { get; set; }

        public Audit()
        {
            Files = new List<File>();
        }
    }
}