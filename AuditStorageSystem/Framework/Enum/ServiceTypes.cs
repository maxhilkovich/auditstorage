using AuditStorageSystem.Framework.Attributes;

namespace AuditStorageSystem.Framework.Enum
{
    public enum ServiceTypes
    {
        [StringMapping("Auto")]
        Auto,
        [StringMapping("BA")]
        BA,
        [StringMapping("Cons")]
        Cons,
        [StringMapping("Sec")]
        Sec,
        [StringMapping("Perf")]
        Perf,
        [StringMapping("Doc")]
        Doc,
        [StringMapping("Telco")]
        Telco,
        [StringMapping("Mob")]
        Mob
    }
}