using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.CQRS.Projects;
using PMS.Application.CQRS.Projects.Comments;
using PMS.Authorization;
using PMS.Pages.Shared;
using RBAC.Application.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCoreApp.Authorization;
using WebApplication1.Models;

namespace PMS.Pages.Projects
{
    public class DetailsModel : BasePageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;
        private readonly IAuthorizationService authorizationService;

        public DetailsModel(WebApplication1.Data.ManageAppDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            this.authorizationService = authorizationService;
        }
        public ProjectViewModel Project { get; set; }
        public List<ProjectCommentViewModel> ListComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, string? search, int p = 1, int s = 3)
        {
            var result = await authorizationService.AuthorizeAsync(User, new Payload { Resource = "PROJECT", ProjectRequirement = new ProjectRequirement() { ProjectId = id, Action = "Read", Resource = "TASK" } }, Operations.Read);
            if (result.Succeeded == false)
                return Unauthorized();
            Project = await Mediator.Send(new GetProjectDetail.Query { ProjectId = id });
            ListComment = await Mediator.Send(new ListProjectComment.Query { SearchTerm = search, PageIndex = p, PageSize = s, ProjectId = id });
            if (ListComment != null)
            {
                Project.ListComment = ListComment;
                await NestedComment(ListComment);
            }
            return Page();

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
    }
}
