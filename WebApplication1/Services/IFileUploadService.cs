using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PMS.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
