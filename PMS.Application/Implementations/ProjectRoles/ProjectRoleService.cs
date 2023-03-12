using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectRoles;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Application.Implementations.ProjectRoles
{
    public class ProjectRoleService : IProjectRoleService
    {
        private readonly IProjectFunctionService projectFunctionService;
        private readonly IProjectPermissionService projectPermissionService;
        private readonly IProjectRoleRepository projectRoleRepository;
        private readonly IProjectPermissionRepository projectPermissionRepository;

        public ProjectRoleService(IProjectFunctionService projectFunctionService, IProjectPermissionService projectPermissionService, IProjectRoleRepository projectRoleRepository, IProjectPermissionRepository projectPermissionRepository)
        {
            this.projectFunctionService = projectFunctionService;
            this.projectPermissionService = projectPermissionService;
            this.projectRoleRepository = projectRoleRepository;
            this.projectPermissionRepository = projectPermissionRepository;
        }

        public Task<bool> CheckPermission(string functionId, string action, string[] roles, int projectId)
        {
            var functions = projectFunctionService.GetAll();
            var permissions = projectPermissionService.GetAll();
            var projectRoles = GetAll().Include(r => r.Project).Where(r => r.ProjectId == projectId);
            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId
                        join r in projectRoles on p.RoleId equals r.Id
                        where roles.Contains(r.Name) && f.Id == functionId
                        && (p.CanCreate && action == "Create"
                        || p.CanUpdate && action == "Update"
                        || p.CanDelete && action == "Delete"
                        || p.CanRead && action == "Read")
                        select p;
            return query.AnyAsync();
        }

        public IQueryable<ProjectRole> GetAll()
        {
            return projectRoleRepository.FindAll();
        }

        public Task<List<ProjectPermission>> GetAllPermission(int id)
        {
            return projectPermissionRepository.FindAll(p => p.RoleId.Equals(id), p => p.ProjectRole).ToListAsync();
            //return _permissionRepository.FindAll(p => p.RoleId.Equals(id.ToString()), p => p.AppRole).ToListAsync();
        }
    }
}
