﻿using PMS.Application.CQRS.Projects;
using PMS.Application.CQRS.Tags;
using PMS.Application.ViewModels;
using PMS.Pages.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.Projects
{
    public class IndexModel : BasePageModel
    {
        public List<TagViewModel> Tags { get; set; }
        public PagedList<ProjectViewModel> Project { get; set; }
        public PaginationParams paginationParams { get; set; } = new PaginationParams();


        public async Task OnGetAsync(string? search, int p = 1, int[] tags = null, int s = 6, bool mine = false)
        {

            Project = await GetProjects(search, p, s, tags, false);
            Tags = await GetTags();

            ViewData["tags"] = tags;

            ViewData["mine"] = mine;
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = Project.MetaData.TotalCount;

        }
        public async Task<PagedList<ProjectViewModel>> GetProjects(string? searchTerm, int page, int pageSize, int[] tags, bool mine)
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
        public async Task OnPostAsync(string search, int p = 1, int s = 6, int[] tags = null, bool mine = false)
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
    }
}
