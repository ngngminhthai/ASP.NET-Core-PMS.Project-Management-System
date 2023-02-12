using PMS.Infrastructure.SharedKernel;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Data.IRepositories
{
    public interface IProjectCommentRepository : IRepository<ProjectComment, int>
    {
    }
}
