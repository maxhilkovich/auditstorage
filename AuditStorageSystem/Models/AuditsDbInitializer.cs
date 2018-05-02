using System.Data.Entity;

namespace AuditStorageSystem.Models
{

    public class AuditsDbInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            context.Projects.AddRange(SamplesData.GetAnyProjects());
            context.Audits.AddRange(SamplesData.GetAnyAudits());
            
            context.Roles.AddRange(SamplesData.GetDefaultRoles());
            context.Users.AddRange(SamplesData.GetUserInRole());
            
            base.Seed(context);
        }
    }
}