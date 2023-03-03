using Microsoft.AspNetCore.Mvc.RazorPages;
using PMS.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace PMS.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {
        private readonly IRoleService _roleService;

        public IndexModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public List<AppRole> Roles { get; set; }

        public async Task OnGetAsync()
        {
            Roles = await _roleService.GetAllAsync();
        }
    }

}
