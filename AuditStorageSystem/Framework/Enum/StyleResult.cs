using AuditStorageSystem.Framework.Attributes;

namespace AuditStorageSystem.Framework.Enum
{
    public enum StyleResult
    {
        [StringMapping("")]
        DatetimePassed,
        [StringMapping("text-danger")]
        DatetimeFailed
    }
}