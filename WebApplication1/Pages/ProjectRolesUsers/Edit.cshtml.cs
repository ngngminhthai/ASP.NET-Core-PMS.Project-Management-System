using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.ProjectRolesUsers
{
    public class EditModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public EditModel(WebApplication1.Data.ManageAppDbContext context)
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
           ViewData["RoleId"] = new SelectList(_context.ProjectRoles, "Id", "Id");
           ViewData["UserId"] = new SelectList(_context.ManageUsers, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProjectRole_User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectRole_UserExists(ProjectRole_User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectRole_UserExists(int id)
        {
            return _context.ProjectRole_Users.Any(e => e.Id == id);
        }
    }
}
