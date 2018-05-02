using AuditStorageSystem.Framework.Attributes;

namespace AuditStorageSystem.Framework.Enum
{
    public enum Colomns
    {
        [StringMapping("Name")]
        ProjectName,
        [StringMapping("Service Type")]
        ServiceType,
        [StringMapping("Last Audit")]
        AuditLastDate,
        [StringMapping("Planned Audit")]
        AuditPlannedDate,
        [StringMapping("Planned Audits")]
        AuditsPlannedDate,
        [StringMapping("Audit Status")]
        AuditStatus,
        [StringMapping("Auditors")]
        Auditors,
        [StringMapping("Result")]
        Result,
        [StringMapping("Actions")]
        Actions,

        [StringMapping("Project Status")]
        ProjectStatus,
        [StringMapping("Unit Coordinator")]
        UnitCoordinator,
        [StringMapping("Unit Manager")]
        UnitManager,
        [StringMapping("Project Coordinator")]
        ProjectCoordinator,
        [StringMapping("Project Manager")]
        ProjectManager,
        [StringMapping("Jira Key")]
        JiraKey,
    }
}