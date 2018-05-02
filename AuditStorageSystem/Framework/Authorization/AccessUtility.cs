using AuditStorageSystem.Framework.Enum;
using AuditStorageSystem.Models;

namespace AuditStorageSystem.Framework.Authorization
{
    public class AccessUtility
    {
        public static bool IsAddResultEnable(DatabaseContext dbContext, string userName)
        {
            var role = AuthorizationUtility.GetUserRole(dbContext, userName);
            return role.Name.ToLower().Equals(nameof(Roles.Admin).ToLower()) ||
                   role.Name.ToLower().Equals(nameof(Roles.SuperAdmin).ToLower()) ||
                   role.Name.ToLower().Equals(nameof(Roles.Auditor).ToLower());
        }

        public static bool IsCreateEnable(DatabaseContext dbContext, string userName)
        {
            var role = AuthorizationUtility.GetUserRole(dbContext, userName);
            return role.Name.ToLower().Equals(nameof(Roles.Admin).ToLower()) || role.Name.ToLower().Equals(nameof(Roles.SuperAdmin).ToLower());
        }
        
        public static bool IsEditEnable(DatabaseContext dbContext, string userName)
        {
            var role = AuthorizationUtility.GetUserRole(dbContext, userName);
            return role.Name.ToLower().Equals(nameof(Roles.Admin).ToLower()) || role.Name.ToLower().Equals(nameof(Roles.SuperAdmin).ToLower());
        }

        public static bool IsDeleteEnable(DatabaseContext dbContext, string userName)
        {
            var role = AuthorizationUtility.GetUserRole(dbContext, userName);
            return role.Name.ToLower().Equals(nameof(Roles.SuperAdmin).ToLower());
        }
    }
}