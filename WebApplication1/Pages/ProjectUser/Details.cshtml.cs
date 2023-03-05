using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PMS.Pages.ProjectUser
{
    public class DetailsModel : PageModel
    {
        //private readonly WebApplication1.Data.ManageAppDbContext _context;

        //public DetailsModel(WebApplication1.Data.ManageAppDbContext context)
        //{
        //    _context = context;
        //}

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
    }
}
