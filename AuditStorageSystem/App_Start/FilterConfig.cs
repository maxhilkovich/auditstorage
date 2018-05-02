using System.Web;
using System.Web.Mvc;
using AuditStorageSystem.Framework.Authorization;

namespace AuditStorageSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomRoleAuthorizeAttribute());
        }
    }
}
