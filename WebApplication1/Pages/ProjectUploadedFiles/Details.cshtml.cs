using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.Projects.ProjectUploadedFiles
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public DetailsModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public ProjectUploadedFile ProjectUploadedFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectUploadedFile = await _context.ProjectUploadedFiles
                .Include(p => p.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (ProjectUploadedFile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
