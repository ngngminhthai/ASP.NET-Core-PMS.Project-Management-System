using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PMS.Services.Implementations
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<string> UploadFile(IFormFile file)
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", currentTime + file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return currentTime + file.FileName;
        }
        public async Task UploadImageForCKEditor(IList<IFormFile> upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            DateTime now = DateTime.Now;
            if (upload.Count == 0)
            {
                //await HttpContext.Response.WriteAsync("Yêu cầu nhập ảnh");
                return;
            }
            else
            {
                var currentTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                var file = upload[0];
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", currentTime + file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }
    }

}
