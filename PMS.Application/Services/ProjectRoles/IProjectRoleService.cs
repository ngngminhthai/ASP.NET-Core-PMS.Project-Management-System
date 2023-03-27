using PMS.Data.Entities.ProjectAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public interface IProjectRoleService
    {
        public Task<bool> CheckPermission(string functionId, string action, string[] roles, int projectId);
        IQueryable<ProjectRole> GetAll();
        public Task<List<ProjectPermission>> GetAllPermission(int id);
        public List<ProjectRole> GetAllRoles(int projectId);
    }
}
