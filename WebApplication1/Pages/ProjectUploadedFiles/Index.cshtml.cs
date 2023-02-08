using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities.ProjectAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Pages.Projects.ProjectUploadedFiles
{

    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public IndexModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public int ProjectId { get; set; }

        public IList<ProjectUploadedFile> ProjectUploadedFile { get; set; }

        public async Task OnGetAsync(int Id)
        {
            System.Console.WriteLine(Id);
            ProjectId = Id;
            System.Console.WriteLine(ProjectId);
            ProjectUploadedFile = await _context.ProjectUploadedFiles
                .Include(p => p.Project).ToListAsync();
        }
    }
}
