using PMS.Data.Entities.ProjectAggregate;
using System.Linq;

namespace PMS.Application.Services
{
    public interface IProjectRole_UserService
    {
        public IQueryable<ProjectRole_User> GetAllProjectRoleUser(string email, int projectId);
    }
}
