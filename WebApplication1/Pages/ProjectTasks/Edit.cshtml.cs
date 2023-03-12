using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Pages.ProjectTasks
{
    public class EditModel : PageModel
    {
        private readonly IProjectTaskService projectTaskService;
        private readonly ManageAppDbContext _context;

        public EditModel(IProjectTaskService projectTaskService, ManageAppDbContext context)
        {
            this.projectTaskService = projectTaskService;
            _context = context;
        }

        [BindProperty]
        public ProjectTask ProjectTask { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectTask = await _context.ProjectTasks
                .Include(p => p.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (ProjectTask == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return Page();
        }

        public IActionResult OnGetUpdateStatus(int id, int workStatus, int pid)
        {
            projectTaskService.UpdateStatus(id, workStatus);
            return RedirectToPage("/ProjectTasks/Index", new { id = pid });
        }

        public IActionResult OnGetUpdatePriority(int id, int priority, int pid)
        {
            projectTaskService.UpdatePriority(id, priority);
            return RedirectToPage("/ProjectTasks/Index", new { id = pid });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int[] dependenciesId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProjectTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTaskExists(ProjectTask.Id))
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

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.Id == id);
        }
    }
}
