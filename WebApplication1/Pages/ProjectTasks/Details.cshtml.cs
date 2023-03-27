using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Pages.ProjectTasks
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;
        private readonly UserManager<ManageUser> usermanager;
        private readonly RoleManager<AppRole> roleManager;

        public DetailsModel(WebApplication1.Data.ManageAppDbContext context, UserManager<ManageUser> usermanager, RoleManager<AppRole> _roleManager)
        {
            _context = context;
            this.usermanager = usermanager;
            roleManager = _roleManager;
        }

        public ProjectTask ProjectTask { get; set; }

        public List<ManageUser> Assignees { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectTask = await _context.ProjectTasks
                .Include(p => p.KanbanColume)
                .Include(p => p.Project).Include(p => p.ProjectTask_Users)
                .FirstOrDefaultAsync(m => m.Id == id);

            var assignees = _context.projectTask_Users
                .Where(ptu => ptu.ProjectTaskId == id)
                .Include(ptu => ptu.User)
                .ThenInclude(u => u.ProjectRole_Users)
                .ThenInclude(pru => pru.ProjectRole)
                .Select(u => new ManageUser
                {
                    UserName = u.User.UserName,
                    Role = string.Join(",", u.User.ProjectRole_Users.Select(pru => pru.ProjectRole.Name))
                })
                .ToList();


            Assignees = assignees;


            if (ProjectTask == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
