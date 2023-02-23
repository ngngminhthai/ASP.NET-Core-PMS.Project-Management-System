using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectRoles;
using System.Linq;
using WebApplication1.Data;

namespace PMS.Application.Implementations.ProjectRoles
{
    public class ProjectRole_UserService : IProjectRole_UserService
    {
        private readonly IProjectRole_UserRepository projectRole_UserRepository;
        private readonly ManageAppDbContext context;

        public ProjectRole_UserService(IProjectRole_UserRepository projectRole_UserRepository, ManageAppDbContext context)
        {
            this.projectRole_UserRepository = projectRole_UserRepository;
            this.context = context;
        }

        public IQueryable<ProjectRole_User> GetAllProjectRoleUser(string email, int projectId)
        {
            var a = context.ProjectRole_Users.Include(p => p.User).Include(p => p.ProjectRole).Where(p => p.User.Email.Equals(email) && p.ProjectRole.ProjectId == projectId);
            return a;
            //projectRole_UserRepository.FindAll(pr => "emlasieunhan118@gmail.com" == email, pr => pr.ProjectRole, pr => pr.User);
        }
    }
}
