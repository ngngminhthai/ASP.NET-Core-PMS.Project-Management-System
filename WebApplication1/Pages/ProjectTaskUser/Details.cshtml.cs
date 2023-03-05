using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.ProjectTaskUser
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public DetailsModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public ProjectTask_User ProjectTask_User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectTask_User = await _context.projectTask_Users
                .Include(p => p.ProjectTask)
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (ProjectTask_User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
