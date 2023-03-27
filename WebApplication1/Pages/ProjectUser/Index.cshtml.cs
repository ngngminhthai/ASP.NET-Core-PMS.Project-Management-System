using Microsoft.AspNetCore.Mvc;
using PMS.Application.CQRS.Projects;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.ProjectUser
{
    public class IndexModel : BasePageModel
    {
        private readonly IProjectUserService projectUserService;
        private readonly IProjectRoleService projectRoleService;
        private readonly ManageAppDbContext context;

        public IndexModel(IProjectUserService projectUserService, IProjectRoleService projectRoleService, ManageAppDbContext context)
        {
            this.projectUserService = projectUserService;
            this.projectRoleService = projectRoleService;
            this.context = context;
        }

        public PaginationParams paginationParams { get; set; } = new PaginationParams();

        public PagedList<ProjectUserViewModel> ListProjectUser { get; set; }
        public List<ProjectRole> ProjectRoles { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        public string Error { get; set; }
        public int ProjectId { get; set; }

        public async Task OnGetAsync(int projectId, string search, int p = 1, int s = 9)
        {
            ProjectRoles = projectRoleService.GetAllRoles(projectId);

            ListProjectUser = await Mediator.Send(new ListProjectUser.Query()
            {
                ProjectId = projectId,
                PageIndex = p,
                PageSize = s,
                SearchTerm = search

            });
            ProjectId = projectId;
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = ListProjectUser.MetaData.TotalCount;

        }
        public async Task OnPostAsync(int projectId)
        {
            var result = projectUserService.Add(projectId, UserName);
            if (result == 1)
            {

                TempData["Error"] = "Can not find User";
            }
            else if (result == 2)
            {

                TempData["Error"] = "User is exist";
            }
            else
            {
                TempData["Error"] = "ok";
            }

            ListProjectUser = await Mediator.Send(new ListProjectUser.Query()
            {
                ProjectId = projectId,
                PageIndex = 1,
                PageSize = 9,
                SearchTerm = null

            });
            ProjectId = projectId;
            paginationParams.PageSize = 1;
            paginationParams.PageNumber = 9;
            paginationParams.Total = ListProjectUser.MetaData.TotalCount;
        }

        public IActionResult OnGetDeleteUser(int projectId, string userId)
        {
            var user = context.ProjectUsers.FirstOrDefault(x => x.ProjectId == projectId && x.UserId == userId);
            context.ProjectUsers.Remove(user);
            context.SaveChanges();
            return Redirect($"Index?projectId={projectId}");
        }
        public IActionResult OnPostUpdateUserRole(int projectId, int project_role_id, string userId)
        {
            var projectRoleUser = context.ProjectRole_Users.FirstOrDefault(pr => pr.RoleId == project_role_id && pr.UserId == userId);
            if (projectRoleUser == null)
                context.ProjectRole_Users.Add(new ProjectRole_User { RoleId = project_role_id, UserId = userId });
            else context.ProjectRole_Users.Update(projectRoleUser);
            context.SaveChanges();
            return Redirect($"/ProjectUser/Index?projectId={projectId}");
        }
    }
}
