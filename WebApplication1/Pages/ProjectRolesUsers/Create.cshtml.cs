using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.ProjectRolesUsers
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public CreateModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoleId"] = new SelectList(_context.ProjectRoles, "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.ManageUsers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ProjectRole_User ProjectRole_User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProjectRole_Users.Add(ProjectRole_User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
