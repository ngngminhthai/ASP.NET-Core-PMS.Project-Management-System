using Microsoft.AspNetCore.Mvc.RazorPages;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.ProjectTasks
{
    public class IndexModel : PageModel
    {
        private readonly IProjectTaskService projectTaskService;
        public PaginationParams paginationParams { get; set; } = new PaginationParams();

        public IndexModel(IProjectTaskService projectTaskService)
        {
            this.projectTaskService = projectTaskService;
        }

        public PagedList<ProjectTaskViewModel> ProjectTask { get; set; }

        public void OnGetAsync(int id, string search, int p = 1, int s = 3)
        {
            ProjectTask = projectTaskService.GetAllWithPagination(id, "", p, s);

            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = ProjectTask.MetaData.TotalCount;
        }
    }
}
