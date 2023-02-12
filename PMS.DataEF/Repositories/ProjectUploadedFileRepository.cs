using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories;
using WebApplication1.Data;

namespace PMS.DataEF.Repositories
{
    public class ProjectUploadedFileRepository : EFRepository<ProjectUploadedFile, int>, IProjectUploadedFileRepository
    {
        public ProjectUploadedFileRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
