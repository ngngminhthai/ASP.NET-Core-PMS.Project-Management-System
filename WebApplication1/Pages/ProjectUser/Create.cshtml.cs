using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PMS.Pages.ProjectUser
{
    public class CreateModel : PageModel
    {
        //private readonly WebApplication1.Data.ManageAppDbContext _context;

        //public CreateModel(WebApplication1.Data.ManageAppDbContext context)
        //{
        //    _context = context;
        //}

        //public IActionResult OnGet()
        //{
        //    ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
        //    ViewData["UserId"] = new SelectList(_context.ManageUsers, "Id", "Id");
        //    return Page();
        //}

        //[BindProperty]
        //public ProjectUser ProjectUser { get; set; }

        //To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.ProjectUsers.Add(ProjectUser);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}
