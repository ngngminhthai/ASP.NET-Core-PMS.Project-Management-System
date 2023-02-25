using PMS.Data.Entities.ProjectAggregate;
using System.Linq;

namespace PMS.Application.Services.ProjectTasks
{
    public interface IProjectTask_UserService
    {
        IQueryable<ProjectTask_User> GetAllTasksOfAllProjectsCurrentUserJoined(string userName);
    }
}
