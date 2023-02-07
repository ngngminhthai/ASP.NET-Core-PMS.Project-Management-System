using PMS.Application.CQRS.Projects;
using PMS.Pages.Shared;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.Projects
{
    public class IndexModel : BasePageModel
    {
        public PagedList<ProjectViewModel> Project { get; set; }
        public PaginationParams paginationParams { get; set; } = new PaginationParams();
        public async Task OnGetAsync(string? search, int p = 1, int s = 6)
        {
            Project = await GetProjects(search, p, s);

            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = Project.MetaData.TotalCount;
        }
        public async Task<PagedList<ProjectViewModel>> GetProjects(string? searchTerm, int page, int pageSize)
        {
            var result = await Mediator.Send(new ListProject.Query()
            {
                SearchTerm = searchTerm,
                PageIndex = page,
                PageSize = pageSize
            });
            return result;
        }

    }
}
