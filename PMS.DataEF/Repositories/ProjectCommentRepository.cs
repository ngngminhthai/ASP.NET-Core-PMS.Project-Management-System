using PMS.Data.IRepositories;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.DataEF.Repositories
{
    public class ProjectCommentRepository : EFRepository<ProjectComment, int>, IProjectCommentRepository
    {
        public ProjectCommentRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
