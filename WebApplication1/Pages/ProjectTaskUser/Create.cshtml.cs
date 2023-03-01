using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.ProjectTaskUser
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
        ViewData["ProjectTaskId"] = new SelectList(_context.ProjectTasks, "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.ManageUsers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ProjectTask_User ProjectTask_User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.projectTask_Users.Add(ProjectTask_User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
