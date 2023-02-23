using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public interface IRoleService
    {
        public Task<bool> CheckPermission(string functionId, string action, string[] roles);
    }
}
