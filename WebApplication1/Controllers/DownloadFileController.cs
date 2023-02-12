using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace PMS.Controllers
{
    public class DownloadFileController : Controller
    {
        [HttpGet]
        public IActionResult DownloadFile(string fileName)
        {
            //get all files in specific directory
            /*var fileNames = Directory.GetFiles(Directory.GetCurrentDirectory() + "/wwwroot/uploads");
            foreach (string wwwfileName in fileNames)
            {
                Console.WriteLine(Path.GetFileName(wwwfileName));
            }*/

            //fileName = "20230122221004689godocker.jpg";

            //Download file based on its name
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(fileName), fileName);
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}
