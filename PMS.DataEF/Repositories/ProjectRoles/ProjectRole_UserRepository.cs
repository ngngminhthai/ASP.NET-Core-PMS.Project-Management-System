using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectRoles;
using WebApplication1.Data;

namespace PMS.DataEF.Repositories.ProjectRoles
{
    public class ProjectRole_UserRepository : EFRepository<ProjectRole_User, int>, IProjectRole_UserRepository
    {
        public ProjectRole_UserRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
