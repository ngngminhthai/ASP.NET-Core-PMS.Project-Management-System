using PMS.Application.ViewModels;
using System.Collections.Generic;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface IProjectTaskService
    {
        void Add(ProjectTaskViewModel project);
        PagedList<ProjectTaskViewModel> GetAllWithPagination(int id, string keyword, int page, int pageSize);
        List<ProjectTaskViewModel> GetAll();
        void Update(ProjectTaskViewModel project);
        void Delete(int id);
        ProjectTaskViewModel GetById(int id);
        void Save();
        void UpdateStatus(int id, int workStatus);
    }
}
