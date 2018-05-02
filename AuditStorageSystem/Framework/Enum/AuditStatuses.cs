using System;
using AuditStorageSystem.Framework.Attributes;

namespace AuditStorageSystem.Framework.Enum
{
    
    public enum AuditStatuses
    {
        [StringMapping("Planned")] Planned,

        [StringMapping("In Progress")] InProgress,

        [StringMapping("Passed")] Passed,

        [StringMapping("Closed")] Closed
    }
}