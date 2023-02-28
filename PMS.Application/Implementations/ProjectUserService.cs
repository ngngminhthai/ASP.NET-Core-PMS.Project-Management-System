using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.IRepositories;
using PMS.Infrastructure.SharedKernel;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class ProjectUserService : IProjectUserService
    {
        private readonly IProjectUserRepository projectUserRepository;
        private readonly ManageAppDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public ProjectUserService(IProjectUserRepository projectUserRepository, IUnitOfWork unitOfWork, ManageAppDbContext context)
        {
            this.projectUserRepository = projectUserRepository;
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public int Add(int projectId, string userName)
        {
            var user = context.Users.Where(u => u.UserName.Equals(userName)).FirstOrDefault();
            if (user == null)
            {
                return 1;
            }
            var userInProject = projectUserRepository.FindAll().Where(p => p.UserId == user.Id && p.ProjectId == projectId).FirstOrDefault();
            if (userInProject != null)
            {
                return 2;
            }
            ProjectUser projectUser = new ProjectUser
            {
                ProjectId = projectId,
                UserId = user.Id,
            };


            projectUserRepository.Add(projectUser);
            Save();
            return 0;
        }

        public PagedList<ProjectUserViewModel> GetAllUserInProject(int projectId, string keyword, int page, int pageSize)
        {
            var query = projectUserRepository.FindAll(p => p.ProjectId == projectId, p => p.Project, p => p.User);

            if (keyword != null)
            {
                query = query.Where(p => p.User.UserName.Contains(keyword));
            }

            return PagedList<ProjectUserViewModel>.ToPagedList(query.ProjectTo<ProjectUserViewModel>(), page, pageSize);

        }
        public void Save()
        {
            unitOfWork.Commit();
        }

    }
}
