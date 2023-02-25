using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectRoles;
using WebApplication1.Data;

namespace PMS.DataEF.Repositories.ProjectRoles
{
    public class ProjectFunctionRepository : EFRepository<ProjectFunction, string>, IProjectFunctionRepository
    {
        public ProjectFunctionRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
