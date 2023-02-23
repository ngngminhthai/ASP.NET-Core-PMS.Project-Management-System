using PMS.Infrastructure.SharedKernel;
using WebApplication1.Data.Entities.UserAggregate;

namespace PMS.Data.IRepositories.SystemRoles
{
    public interface IPermissionRepository : IRepository<Permission, int>
    {
    }
}
