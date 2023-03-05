using PMS.Application.ViewModels;
using PMS.Data.Entities.ProjectAggregate;
using System.Collections.Generic;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface IProjectTaskService
    {
        void Add(ProjectTask project);
        PagedList<ProjectTaskViewModel> GetAllWithPagination(int id, string keyword, int page, int pageSize);
        List<ProjectTaskViewModel> GetUpcommingTaskOfUser(string userid);
        List<ProjectTask_User> GetAllProjecTaskUser();
        List<ProjectTaskViewModel> GetAll();
        void Update(ProjectTaskViewModel project);
        void Delete(int id);
        ProjectTaskViewModel GetById(int id);
        void Save();
        void UpdateStatus(int id, int workStatus);
        void UpdatePriority(int id, int priorityValue);
    }
}
