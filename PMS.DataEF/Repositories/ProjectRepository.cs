using PMS.Data.IRepositories;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.DataEF.Repositories
{
    public class ProjectRepository : EFRepository<Project, int>, IProjectRepository
    {
        public ProjectRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
