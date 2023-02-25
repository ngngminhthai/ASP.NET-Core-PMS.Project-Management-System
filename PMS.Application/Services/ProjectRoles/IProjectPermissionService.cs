using PMS.Data.Entities.ProjectAggregate;
using System.Linq;

namespace PMS.Application.Services
{
    public interface IProjectPermissionService
    {
        IQueryable<ProjectPermission> GetAll();
    }
}
