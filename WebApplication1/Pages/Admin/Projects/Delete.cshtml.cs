using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Pages.Admin.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public DeleteModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FindAsync(id);

            if (Project != null)
            {
                _context.Projects.Remove(Project);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
