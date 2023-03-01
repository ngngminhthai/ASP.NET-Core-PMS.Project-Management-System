using Microsoft.AspNetCore.Mvc;
using PMS.Application.CQRS.Projects;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Pages.Shared;
using System.Threading.Tasks;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.ProjectUser
{
    public class IndexModel : BasePageModel
    {
        private readonly IProjectUserService projectUserService;

        public IndexModel(IProjectUserService projectUserService)
        {
            this.projectUserService = projectUserService;
        }

        public PaginationParams paginationParams { get; set; } = new PaginationParams();

        public PagedList<ProjectUserViewModel> ListProjectUser { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        public bool? Flag { get; set; } = null;
        public string Error { get; set; }
        public int ProjectId { get; set; }
        public void SetFlagNull()
        {
            Flag = null;
        }
        public async Task OnGetAsync(int projectId, string search, int p = 1, int s = 9)
        {
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
        public async Task<IActionResult> OnPostAsync(int projectId)
        {
            var result = projectUserService.Add(projectId, UserName);
            if (result == 1)
            {
                Flag = false;
                Error = "Can not find User";
            }
            else if (result == 2)
            {
                Flag = false;
                Error = "User is exist";
            }
            else
            {
                Flag = true;
            }
            return Redirect("projectUser");

        }
    }
}
