using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PMS.Pages.ProjectUser
{
    public class DeleteModel : PageModel
    {
        //private readonly WebApplication1.Data.ManageAppDbContext _context;

        //public DeleteModel(WebApplication1.Data.ManageAppDbContext context)
        //{
        //    _context = context;
        //}

        //[BindProperty]
        //public ProjectUser ProjectUser { get; set; }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    ProjectUser = await _context.ProjectUsers
        //        .Include(p => p.Project)
        //        .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

        //    if (ProjectUser == null)
        //    {
        //        return NotFound();
        //    }
        //    return Page();
        //}

        //public async Task<IActionResult> OnPostAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    ProjectUser = await _context.ProjectUsers.FindAsync(id);

        //    if (ProjectUser != null)
        //    {
        //        _context.ProjectUsers.Remove(ProjectUser);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToPage("./Index");
        //}
    }
}
