using PMS.Application.ViewModels;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface IProjectUserService
    {

        public PagedList<ProjectUserViewModel> GetAllUserInProject(int projectId, string keyword, int page, int pageSize);
        public int Add(int projectId, string UserName);

    }
}
