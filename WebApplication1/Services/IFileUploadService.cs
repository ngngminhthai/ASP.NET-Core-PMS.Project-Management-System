using Microsoft.AspNetCore.Http;
using PMS.Application.ViewModels;
using System.Threading.Tasks;

namespace PMS.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file, UploadedFileViewModel uploadedFileViewModel);
    }
}
