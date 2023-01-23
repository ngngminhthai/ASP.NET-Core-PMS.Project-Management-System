using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PMS.Controllers
{
    public class CKEditorUpload : BaseController
    {
        [HttpPost]
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
                var imageFolder = $@"\uploads\";
                var filename = currentTime + file.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                await HttpContext.Response.WriteAsync("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + Path.Combine(imageFolder, filename).Replace(@"\", @"/") + "');</script>");
            }
        }
    }
}
