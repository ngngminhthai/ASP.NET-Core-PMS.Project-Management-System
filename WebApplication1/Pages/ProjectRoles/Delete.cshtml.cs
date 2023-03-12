using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.ProjectRoles
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public DeleteModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProjectRole ProjectRole { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectRole = await _context.ProjectRoles
                .Include(p => p.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (ProjectRole == null)
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

            ProjectRole = await _context.ProjectRoles.FindAsync(id);

            if (ProjectRole != null)
            {
                _context.ProjectRoles.Remove(ProjectRole);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
