using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Pages.Gantt
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public IndexModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public IList<ProjectTask> ProjectTask { get;set; }

        public async Task OnGetAsync()
        {
            ProjectTask = await _context.ProjectTasks
                .Include(p => p.KanbanColume)
                .Include(p => p.Project).ToListAsync();
        }
    }
}
