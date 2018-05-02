using System.Collections.Generic;

namespace AuditStorageSystem.Models.Authorization
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Domain { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }

    }
}