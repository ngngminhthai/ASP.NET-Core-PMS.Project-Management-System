using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.CQRS.ProjectUploadedFiles;
using PMS.Application.ViewModels;
using PMS.Pages.Shared;
using PMS.Services;
using System.Threading.Tasks;

namespace PMS.Pages.Projects.ProjectUploadedFiles
{
    public class CreateModel : BasePageModel
    {
        private readonly IFileUploadService fileUploadService;

        public CreateModel(IFileUploadService fileUploadService)
        {
            this.fileUploadService = fileUploadService;
        }

        public IActionResult OnGet(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            System.Console.WriteLine(id);
            ProjectId = id;
            return Page();
        }

        [BindProperty]
        public ProjectUploadedFileViewModel ProjectUploadedFile { get; set; }

        [BindProperty]
        public int ProjectId { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UploadedFileViewModel uploadedFileViewModel = new UploadedFileViewModel { UploaderName = HttpContext.User.Identity.Name, UploadedPosition = Data.Enums.UploadedPosition.Project };
            var filePath = await fileUploadService.UploadFile(file, uploadedFileViewModel);
            ProjectUploadedFile.File = filePath;

            await Mediator.Send(new AddUploadedFiles.Command { ProjectId = this.ProjectId, projectUploadedFileViewModel = this.ProjectUploadedFile });

            return RedirectToPage("./Index");
        }
    }
}
