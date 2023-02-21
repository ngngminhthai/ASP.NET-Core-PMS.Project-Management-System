using System.Collections.Generic;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface IProjectCommentService
    {
        void Add(ProjectComment comment);
        public List<ProjectCommentViewModel> GetChildComments(int parentId);

        public ProjectComment GetCommentById(int id);

        PagedList<ProjectCommentViewModel> GetAllWithPagination(int page, int pageSize, int? projectId);
        List<ProjectCommentViewModel> GetAll();
        void Update(int id , string content);
        void Delete(int id);
        ProjectCommentViewModel GetById(int id);
        void Save();
    }
}
