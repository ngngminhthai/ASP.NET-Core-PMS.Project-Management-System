using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectRoles;
using WebApplication1.Data;

namespace PMS.DataEF.Repositories.ProjectRoles
{
    public class ProjectPermissionRepository : EFRepository<ProjectPermission, int>, IProjectPermissionRepository
    {
        public ProjectPermissionRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
