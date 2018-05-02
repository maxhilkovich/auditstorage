using System;
using AuditStorageSystem.Framework.Enum;
using AuditStorageSystem.Framework.Extensions;
using AuditStorageSystem.Models;

namespace AuditStorageSystem.Framework
{
    public class HelperForAudits
    {
        public static string CheckAuditRelevance(Audit audit)
        {
            //if (audit.LastAuditDate == null)
            //{
            //    return DateTime.Compare(audit.PlannedDate, DateTime.Now) >= 0
            //        ? StyleResult.DatetimePassed.GetStringMapping()
            //        : StyleResult.DatetimeFailed.GetStringMapping();
            //}
            //return DateTime.Compare(audit.PlannedDate, audit.LastAuditDate.Value) >= 0
            //    ? StyleResult.DatetimePassed.GetStringMapping()
            //    : StyleResult.DatetimeFailed.GetStringMapping();

            var plannedDiff = (DateTime.Now - audit.PlannedDate).Days;
            if ((audit.Status == AuditStatuses.InProgress && plannedDiff > 30) ||
                (audit.Status == AuditStatuses.Passed && audit.LastAuditDate != null &&
                 (DateTime.Now - audit.LastAuditDate.Value).Days > 30))
            {
                return StyleResult.DatetimeFailed.GetStringMapping();
            }
            
            return (DateTime.Compare(audit.PlannedDate, DateTime.Now) <= 0) &&
                !(audit.Status == AuditStatuses.InProgress || audit.Status == AuditStatuses.Passed || audit.Status == AuditStatuses.Closed)
                    ? StyleResult.DatetimeFailed.GetStringMapping()
                    : StyleResult.DatetimePassed.GetStringMapping();
        }

        public static string GetAuditStyleForCalendar(Audit audit)
        {
            switch (audit.Status)
            {
                case AuditStatuses.Planned:
                    return AuditStyleForCalendar.Planned.GetStringMapping();
                case AuditStatuses.InProgress:
                    return AuditStyleForCalendar.InProgress.GetStringMapping();
                case AuditStatuses.Passed:
                    return AuditStyleForCalendar.Passed.GetStringMapping();
                case AuditStatuses.Closed:
                    return AuditStyleForCalendar.Closed.GetStringMapping();
                default: throw new NotImplementedException();
            }
        }

    }
}