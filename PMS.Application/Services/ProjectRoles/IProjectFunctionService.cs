using PMS.Data.Entities.ProjectAggregate;
using System.Linq;

namespace PMS.Application.Services
{
    public interface IProjectFunctionService
    {
        IQueryable<ProjectFunction> GetAll();
    }
}
