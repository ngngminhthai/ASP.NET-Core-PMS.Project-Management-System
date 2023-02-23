using PMS.Data.IRepositories.SystemRoles;
using WebApplication1.Data;
using WebApplication1.Data.Entities.UserAggregate;

namespace PMS.DataEF.Repositories
{
    public class PermissionRepository : EFRepository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
