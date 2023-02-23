using PMS.Application.Services;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectRoles;
using System.Linq;

namespace PMS.Application.Implementations.ProjectRoles
{
    public class ProjectFunctionService : IProjectFunctionService
    {
        private readonly IProjectFunctionRepository projectFunctionRepository;

        public ProjectFunctionService(IProjectFunctionRepository projectFunctionRepository)
        {
            this.projectFunctionRepository = projectFunctionRepository;
        }

        public IQueryable<ProjectFunction> GetAll()
        {
            return projectFunctionRepository.FindAll();
        }
    }
}
