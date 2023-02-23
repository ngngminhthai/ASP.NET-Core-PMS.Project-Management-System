using PMS.Data.IRepositories.SystemRoles;
using WebApplication1.Data;
using WebApplication1.Data.Entities;

namespace PMS.DataEF.Repositories
{
    public class FunctionRepository : EFRepository<Function, string>, IFunctionRepository
    {
        public FunctionRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
