using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PMS.Pages.ProjectUser
{
    public class EditModel : PageModel
    {
        //private readonly WebApplication1.Data.ManageAppDbContext _context;

        //public EditModel(WebApplication1.Data.ManageAppDbContext context)
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
        //   ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
        //   ViewData["UserId"] = new SelectList(_context.ManageUsers, "Id", "Id");
        //    return Page();
        //}

        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Attach(ProjectUser).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectUserExists(ProjectUser.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("./Index");
        //}

        //private bool ProjectUserExists(int id)
        //{
        //    return _context.ProjectUsers.Any(e => e.Id == id);
        //}
    }
}
