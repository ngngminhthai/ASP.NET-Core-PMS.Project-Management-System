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
        // private readonly IHubContext<SignalSever> _signalrHub;
        public DetailsModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
            //_signalrHub = signalrHub;
        }
        public ProjectViewModel Project { get; set; }
        public List<ProjectCommentViewModel> ListComment { get; set; }

        public async Task OnGetAsync(int id, string? search, int p = 1, int s = 3)
        {

            Project = await Mediator.Send(new GetProjectDetail.Query { ProjectId = id });
            ListComment = await getListComment(id, search, p, s);


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
    }
}
