using PMS.Data.IRepositories;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.DataEF.Repositories
{
    public class ProjectTaskRepository : EFRepository<ProjectTask, int>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
