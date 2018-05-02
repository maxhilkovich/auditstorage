using AuditStorageSystem.Framework.Attributes;

namespace AuditStorageSystem.Framework.Enum
{
    public enum ProjectStatuses
    {
        [StringMapping("Active")] Active,

        [StringMapping("Closed")] Closed
    }
}