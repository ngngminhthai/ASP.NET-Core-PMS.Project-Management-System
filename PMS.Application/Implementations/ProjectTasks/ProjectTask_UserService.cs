using PMS.Application.Services.ProjectTasks;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectTasks;
using System.Linq;

namespace PMS.Application.Implementations.ProjectTasks
{
    public class ProjectTask_UserService : IProjectTask_UserService
    {
        private readonly IProjectTask_UserRepository projectTask_UserRepository;

        public ProjectTask_UserService(IProjectTask_UserRepository projectTask_UserRepository)
        {
            this.projectTask_UserRepository = projectTask_UserRepository;
        }

        public IQueryable<ProjectTask_User> GetAllTasksOfAllProjectsCurrentUserJoined(string userName)
        {
            return projectTask_UserRepository.FindAll(p => p.User.UserName.Equals(userName), p => p.User, p => p.ProjectTask, p => p.ProjectTask.Project);
        }
    }
}
