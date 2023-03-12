using Microsoft.AspNetCore.Mvc;
using PMS.Application.CQRS.Projects;
using PMS.Application.CQRS.Projects.Comments;
using PMS.Application.CQRS.Tags;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Pages.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;

namespace PMS.Pages.Projects
{
    public class DetailsModel : BasePageModel
    {
        private readonly IProjectService projectService;

        public DetailsModel(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        public List<TagViewModel> Tags { get; set; }
        public ProjectViewModel Project { get; set; }
        public List<ProjectCommentViewModel> ListComment { get; set; }
        public string Email { get; set; }
       
        public async Task OnGetAsync(int id, string? search, int p = 1, int s = 3)
        {
            Email = HttpContext.User.Identity.Name.ToString();
            Project = await Mediator.Send(new GetProjectDetail.Query { ProjectId = id });
            ListComment = await getListComment(id, search, p, s);
            Tags = await GetTags();

        }
        public async Task<List<TagViewModel>> GetTags()
        {
            var result = await Mediator.Send(
            new ListTag.Query() { }
            );
            return result;
        }
        public async Task<List<ProjectCommentViewModel>> NestedComment(List<ProjectCommentViewModel> listChildComment)
        {

            if (listChildComment != null)
            {

                foreach (ProjectCommentViewModel comment in listChildComment)
                {
                    var list = await Mediator.Send(new GetListChildComment.Query { ParentId = comment.Id });
                    comment.ChildComments = await NestedComment(list);
                }
                return listChildComment;
            }
            return null;

        }
        public async Task<List<ProjectCommentViewModel>> getListComment(int id, string? search, int page, int pageSize)
        {
            List<ProjectCommentViewModel> listCmt = await Mediator.Send(new ListProjectComment.Query { PageIndex = page, PageSize = pageSize, ProjectId = id });

            await NestedComment(listCmt);

            return listCmt;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int projectId)
        {
            projectService.Delete(projectId);

            return Redirect("../Projects");
           // return Page();
        }
        public async Task<IActionResult> OnPostEditAsync(Project project)
        {
            projectService.Update(project);

            return Redirect("../Projects/Details?id="+project.Id);
            // return Page();
        }
    }
}
