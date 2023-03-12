using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;

namespace PMS.Pages.ProjectRolesUsers
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public IndexModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        public IList<ProjectRole_User> ProjectRole_User { get;set; }

        public async Task OnGetAsync()
        {
            ProjectRole_User = await _context.ProjectRole_Users
                .Include(p => p.ProjectRole)
                .Include(p => p.User).ToListAsync();
        }
    }
}
