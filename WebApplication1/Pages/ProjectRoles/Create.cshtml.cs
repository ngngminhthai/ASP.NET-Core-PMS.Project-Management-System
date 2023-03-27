using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMS.Data.Entities.ProjectAggregate;
using System.Threading.Tasks;

namespace PMS.Pages.ProjectRoles
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public CreateModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            //ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            ViewData["ProjectRoleId"] = id;
            return Page();
        }

        [BindProperty]
        public ProjectRole ProjectRole { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProjectRoles.Add(ProjectRole);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = ProjectRole.ProjectId });
        }
    }
}
