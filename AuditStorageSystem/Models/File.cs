using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using MySql.Data.MySqlClient;

namespace AuditStorageSystem.Models
{
    [DbConfigurationType(typeof(MySqlConfiguration))]
    public class File
    {
        public int FileId { get; set; }
        public int AuditId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}