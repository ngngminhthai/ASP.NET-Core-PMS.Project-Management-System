using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities.ProjectAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Pages.ProjectRoles
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public IndexModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public IList<ProjectRole> ProjectRole { get; set; }

        public async Task OnGetAsync(int id)
        {
            var project = _context.Projects.Find(id);
            ProjectRole = await _context.ProjectRoles
                .Include(p => p.Project).Where(pr => pr.ProjectId == id).ToListAsync();

            ViewData["Project"] = project;

        }
    }
}
