using PMS.Application.ViewModels;
using PMS.Data.Entities.ProjectAggregate;
using System.Linq;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services.ProjectTasks
{
    public interface IProjectTask_UserService
    {
        IQueryable<ProjectTask_User> GetAllTasksOfAllProjectsCurrentUserJoined(string userName);
        public PagedList<ProjectTask_User> GetAllUserInTask(int projectTaskId, string keyword, int page, int pageSize);
        public int Add(int projectId, string UserName);
    }
}
