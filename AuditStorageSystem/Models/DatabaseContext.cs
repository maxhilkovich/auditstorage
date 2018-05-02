using System.Data.Entity;
using AuditStorageSystem.Models.Authorization;
using MySql.Data.Entity;

namespace AuditStorageSystem.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("MySQLConnection")
        {
        }

        public DbSet<Audit> Audits { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<File> Files { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }


}
