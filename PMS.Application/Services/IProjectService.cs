
using System.Collections.Generic;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface IProjectService
    {
        void Add(Project project);
        PagedList<ProjectViewModel> GetAllWithPagination(string keyword, int page, int pageSize);
        List<ProjectViewModel> GetAll();
        void Update(Project project);
        void Delete(int id);
        ProjectViewModel GetById(int id);
        void Save();

    }
}
