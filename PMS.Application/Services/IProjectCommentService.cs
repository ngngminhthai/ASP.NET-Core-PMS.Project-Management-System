using System.Collections.Generic;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface IProjectCommentService
    {
        void Add(ProjectComment comment);
        public List<ProjectCommentViewModel> GetChildComments(int parentId);
        PagedList<ProjectCommentViewModel> GetAllWithPagination(string keyword, int page, int pageSize, int? projectId);
        List<ProjectCommentViewModel> GetAll();
        void Update(ProjectCommentViewModel comment);
        void Delete(int id);
        ProjectCommentViewModel GetById(int id);
        void Save();
    }
}
