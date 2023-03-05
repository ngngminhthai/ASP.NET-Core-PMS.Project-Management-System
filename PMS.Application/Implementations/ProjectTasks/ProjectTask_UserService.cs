using PMS.Application.Services.ProjectTasks;
using PMS.Application.ViewModels;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories;
using PMS.Data.IRepositories.ProjectTasks;
using PMS.DataEF;
using PMS.Infrastructure.SharedKernel;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations.ProjectTasks
{
    public class ProjectTask_UserService : IProjectTask_UserService
    {
        private readonly IProjectTask_UserRepository projectTask_UserRepository;
        private readonly ManageAppDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public ProjectTask_UserService(IProjectTask_UserRepository projectTask_UserRepository, ManageAppDbContext context, IUnitOfWork unitOfWork)
        {
            this.projectTask_UserRepository = projectTask_UserRepository;
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        public int Add(int projectTaskId, string userName)
        {
            var user = context.Users.Where(u => u.UserName.Equals(userName)).FirstOrDefault();
            if (user == null)
            {
                return 1;
            }
            var userInProject = projectTask_UserRepository.FindAll().Where(p => p.UserId == user.Id && p.ProjectTaskId == projectTaskId).FirstOrDefault();
            if (userInProject != null)
            {
                return 2;
            }
            ProjectTask_User projectUser = new ProjectTask_User
            {
               ProjectTaskId= projectTaskId,
               UserId= user.Id,
            };


            projectTask_UserRepository.Add(projectUser);
            Save();
            return 0;
        }

        public IQueryable<ProjectTask_User> GetAllTasksOfAllProjectsCurrentUserJoined(string userName)
        {
            return projectTask_UserRepository.FindAll(p => p.User.UserName.Equals(userName), p => p.User, p => p.ProjectTask, p => p.ProjectTask.Project);
        }

        public PagedList<ProjectTask_User> GetAllUserInTask(int projectTaskId, string keyword, int page, int pageSize)
        {
            var query = projectTask_UserRepository.FindAll(p => p.ProjectTaskId == projectTaskId, p => p.ProjectTask, p => p.User);

            if (keyword != null)
            {
                query = query.Where(p => p.User.UserName.Contains(keyword));
            }

            return PagedList<ProjectTask_User>.ToPagedList(query, page, pageSize);

        }
        public void Save()
        {
            unitOfWork.Commit();
        }

    }
}
