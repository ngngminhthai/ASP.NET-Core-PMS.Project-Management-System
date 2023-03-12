using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Data.Entities.ProjectAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Pages.ProjectRoles
{
    public class EditModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;
        private readonly IProjectFunctionService projectFunctionService;
        private readonly IProjectPermissionService projectPermissionService;
        private readonly IProjectRoleService projectRoleService;

        public EditModel(WebApplication1.Data.ManageAppDbContext context, IProjectFunctionService projectFunctionService, IProjectPermissionService projectPermissionService, IProjectRoleService projectRoleService)
        {
            _context = context;
            this.projectFunctionService = projectFunctionService;
            this.projectPermissionService = projectPermissionService;
            this.projectRoleService = projectRoleService;
        }

        [BindProperty]
        public ProjectRole ProjectRole { get; set; }

        [BindProperty]
        public List<ProjectFunction> Functions { get; set; }

        [BindProperty]
        public List<ProjectPermission> Permissions { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectRole = await _context.ProjectRoles
                .Include(p => p.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (ProjectRole == null)
            {
                return NotFound();
            }

            Functions = projectFunctionService.GetAll().ToList();

            Permissions = await projectRoleService.GetAllPermission((int)id);


            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProjectRoles.Update(ProjectRole);

            foreach (var permission in Permissions)
            {

                if (_context.ProjectPermissions.Any(p => p.FunctionId == permission.FunctionId && p.RoleId == permission.RoleId))
                {
                    _context.Attach(permission).State = EntityState.Modified;
                }
                else _context.Attach(permission).State = EntityState.Added;
            }

            var changes = _context.ChangeTracker.Entries();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return RedirectToPage(new { id = ProjectRole.Id });
        }

        private bool ProjectRoleExists(int id)
        {
            return _context.ProjectRoles.Any(e => e.Id == id);
        }
    }
}
