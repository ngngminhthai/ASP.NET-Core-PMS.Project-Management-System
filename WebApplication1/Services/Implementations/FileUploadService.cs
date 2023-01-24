using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PMS.Services.Implementations
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return "No file was uploaded.";
            }
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", currentTime + file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return currentTime + file.FileName;
        }
    }

}
