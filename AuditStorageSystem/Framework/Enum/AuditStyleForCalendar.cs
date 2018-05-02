using AuditStorageSystem.Framework.Attributes;

namespace AuditStorageSystem.Framework.Enum
{
    public enum AuditStyleForCalendar
    {
        [StringMapping("audit-planned")]
        Planned,

        [StringMapping("audit-in-progress")]
        InProgress,

        [StringMapping("audit-passed")]
        Passed,

        [StringMapping("audit-clossed")]
        Closed
    }
}