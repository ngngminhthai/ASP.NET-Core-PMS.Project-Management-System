using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMS.Services;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace PMS.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public CreateModel(WebApplication1.Data.ManageAppDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            this._fileUploadService = fileUploadService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var filePath = await _fileUploadService.UploadFile(file);
            Product.Image = filePath;
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
