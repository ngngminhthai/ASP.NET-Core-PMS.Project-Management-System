using Microsoft.AspNetCore.Mvc;

using PMS.Application.CQRS.Projects;
using PMS.Application.CQRS.Tags;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Pages.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.Projects
{
    public class IndexModel : BasePageModel
    {
        private readonly IProjectService projectService;

        public IndexModel(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        public List<TagViewModel> Tags { get; set; }
        public PagedList<ProjectViewModel> Project { get; set; }
        public PaginationParams paginationParams { get; set; } = new PaginationParams();
        [BindProperty]
        public Project ProjectInput { get; set; }
        public async Task OnGetAsync(string? search, int p = 1, int[] tags = null, int s = 3, bool mine = false)
        {

            Project = await GetProjects(search, p, s, tags, false);
            Tags = await GetTags();

            ViewData["tags"] = tags;

            ViewData["mine"] = mine;
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = Project.MetaData.TotalCount;

        }
        public async Task<PagedList<ProjectViewModel>> GetProjects(string searchTerm, int page, int pageSize, int[] tags, bool mine)
        {
            var email = HttpContext.User.Identity.Name;
            var result = await Mediator.Send(new ListProject.Query()
            {
                SearchTerm = searchTerm,
                PageIndex = page,
                PageSize = pageSize,
                Email = email,
                Tag = tags,
                Mine = mine,
            }); ;

            return result;
        }
        public async Task<List<TagViewModel>> GetTags()
        {
            var result = await Mediator.Send(
            new ListTag.Query() { }
            );
            return result;
        }
        public async Task OnPostAsync(string search, int p = 1, int s = 3, int[] tags = null, bool mine = false)
        {


            Project = await GetProjects(search, p, s, tags, mine);
            Tags = await GetTags();

            ViewData["tags"] = tags;

            ViewData["mine"] = mine;
            ViewData["search"] = search;
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = Project.MetaData.TotalCount;
        }

        public async Task<IActionResult> OnPostCreateAsync(int tagInput, int[] tags = null)
        {
            var email = HttpContext.User.Identity.Name;

            projectService.Add(ProjectInput, email);


            Project = await GetProjects(null, 1, 6, tags, false);
            Tags = await GetTags();

            ViewData["tags"] = tags;

            ViewData["mine"] = false;
            ViewData["search"] = null;
            paginationParams.PageSize = 6;
            paginationParams.PageNumber = 1;
            paginationParams.Total = Project.MetaData.TotalCount;
            return Redirect("Projects");
        }
    }
}
