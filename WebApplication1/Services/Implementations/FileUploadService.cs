using Microsoft.AspNetCore.Http;
using PMS.Application.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PMS.Services.Implementations
{
    public class FileUploadService : IFileUploadService
    {

        public async Task<string> UploadFile(IFormFile file, UploadedFileViewModel uploadedFileViewModel)
        {
            /*            var uploadedFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uploadedFileViewModel.UploaderName);
            */
            var uploadedFolder = "";
            if (file == null)
            {
                return "No file was uploaded.";
            }
            /*if (!Directory.Exists(uploadedFolder))
            {
                Directory.CreateDirectory(uploadedFolder);
            }*/
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uploadedFolder, currentTime + file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return currentTime + file.FileName;
        }
    }

}
