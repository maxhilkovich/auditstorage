using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using AuditStorageSystem.Models;

namespace AuditStorageSystem.Framework.Authorization
{
    public class CustomRoleAuthorizeAttribute : AuthorizeAttribute
    {
        private DatabaseContext db = new DatabaseContext();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var foundUser = AuthorizationUtility.GetUserFromAuthTable(db, httpContext.User.Identity.Name);

            if (!foundUser.Any())
                return false;

            if (Roles.IsEmpty())
                return true;

            var foundUserRole = AuthorizationUtility.GetUserRole(db, foundUser.First());
            var availableRoles = Roles.Split(',').ToList();
            return availableRoles.Contains(foundUserRole.Name);
        }
    }
}