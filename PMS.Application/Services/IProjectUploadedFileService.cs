using PMS.Application.ViewModels;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface IProjectUploadedFileService
    {
        void Add(int ProjectId, ProjectUploadedFileViewModel FileName);
        PagedList<ProjectViewModel> GetAllWithPagination(string keyword, int page, int pageSize, string email);
    }
}
