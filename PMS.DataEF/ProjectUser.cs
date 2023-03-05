using PMS.Data.IRepositories;
using PMS.DataEF.Repositories;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.DataEF
{
    public class ProjectUserRepository : EFRepository<ProjectUser, int>, IProjectUserRepository
    {
        public ProjectUserRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
