using PMS.Application.CQRS.Projects;
using PMS.Application.CQRS.Projects.Comments;
using PMS.Pages.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace PMS.Pages.Projects
{
    public class DetailsModel : BasePageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public DetailsModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }
        public ProjectViewModel Project { get; set; }
        public List<ProjectCommentViewModel> ListComment { get; set; }

        public async Task OnGetAsync(int id, string? search, int p = 1, int s = 3)
        {

            Project = await Mediator.Send(new GetProjectDetail.Query { ProjectId = id });
            ListComment = await Mediator.Send(new ListProjectComment.Query { SearchTerm = search, PageIndex = p, PageSize = s, ProjectId = id });
            if (ListComment != null)
            {
                Project.ListComment = ListComment;
                await NestedComment(ListComment);
            }


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
