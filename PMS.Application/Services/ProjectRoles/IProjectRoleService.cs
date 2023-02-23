using PMS.Data.Entities.ProjectAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public interface IProjectRoleService
    {
        public Task<bool> CheckPermission(string functionId, string action, string[] roles, int projectId);
        IQueryable<ProjectRole> GetAll();
    }
}
