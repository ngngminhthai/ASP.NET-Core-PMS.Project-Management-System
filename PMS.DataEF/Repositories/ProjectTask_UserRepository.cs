using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectTasks;
using WebApplication1.Data;

namespace PMS.DataEF.Repositories
{
    public class ProjectTask_UserRepository : EFRepository<ProjectTask_User, int>, IProjectTask_UserRepository
    {
        public ProjectTask_UserRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
