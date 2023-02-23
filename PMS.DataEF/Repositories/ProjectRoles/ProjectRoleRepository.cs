using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectRoles;
using WebApplication1.Data;

namespace PMS.DataEF.Repositories.ProjectRoles
{
    public class ProjectRoleRepository : EFRepository<ProjectRole, int>, IProjectRoleRepository
    {
        public ProjectRoleRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
