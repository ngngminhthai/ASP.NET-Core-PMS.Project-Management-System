using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.ProjectRolesUsers
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public DeleteModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProjectRole_User ProjectRole_User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectRole_User = await _context.ProjectRole_Users
                .Include(p => p.ProjectRole)
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (ProjectRole_User == null)
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

            ProjectRole_User = await _context.ProjectRole_Users.FindAsync(id);

            if (ProjectRole_User != null)
            {
                _context.ProjectRole_Users.Remove(ProjectRole_User);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
