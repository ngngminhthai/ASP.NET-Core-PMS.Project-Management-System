using PMS.Application.Services;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectRoles;
using System.Linq;

namespace PMS.Application.Implementations.ProjectRoles
{
    public class ProjectPermissionService : IProjectPermissionService
    {
        private readonly IProjectPermissionRepository projectPermissionRepository;

        public ProjectPermissionService(IProjectPermissionRepository projectPermissionRepository)
        {
            this.projectPermissionRepository = projectPermissionRepository;
        }

        public IQueryable<ProjectPermission> GetAll()
        {
            return projectPermissionRepository.FindAll();
        }
    }
}
