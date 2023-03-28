using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Application.Services.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.UserAggregate;

namespace PMS.Pages.Admin.Roles
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IFunctionService functionService;
        private readonly IRoleService roleService;
        private readonly ManageAppDbContext _context;

        public EditModel(IFunctionService functionService, IRoleService roleService, ManageAppDbContext context)
        {
            this.functionService = functionService;
            this.roleService = roleService;
            this._context = context;
        }

        [BindProperty]
        public AppRole AppRole { get; set; } = default!;

        [BindProperty]
        public List<Function> Functions { get; set; }

        [BindProperty]
        public List<Permission> Permissions { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {

            var approle = await roleService.GetById(id);

            if (approle == null)
            {
                return NotFound();
            }
            AppRole = approle;

            Functions = functionService.GetAll();

            Permissions = await roleService.GetAllPermission(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await roleService.UpdateAsync(AppRole);

            foreach (var permission in Permissions)
            {

                if (_context.Permissions.Any(p => p.FunctionId == permission.FunctionId && p.RoleId == permission.RoleId))
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

            return RedirectToPage(new { id = AppRole.Id });
        }
    }
}
