namespace PMS.Pages.Projects
{
    public class DetailsModel : BasePageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;
        private readonly IAuthorizationService authorizationService;

        // private readonly IHubContext<SignalSever> _signalrHub;
        public DetailsModel(WebApplication1.Data.ManageAppDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
        }
        public ProjectViewModel Project { get; set; }
        public ProjectViewModel Project { get; set; }
        //_signalrHub = signalrHub;
        public List<ProjectCommentViewModel> ListComment { get; set; }

        public async Task OnGetAsync(int id, string? search, int p = 1, int s = 3)
        {
            var result = await authorizationService.AuthorizeAsync(User, new Payload { Resource = "PROJECT", ProjectRequirement = new ProjectRequirement() { ProjectId = id, Action = "Read", Resource = "TASK" } }, Operations.Read);
        public string Email { get; set; }
        public async Task OnGetAsync(int id, string? search, int p = 1, int s = 3)
            Project = await Mediator.Send(new GetProjectDetail.Query { ProjectId = id
    });
            Email = HttpContext.User.Identity.Name.ToString();
                return Unauthorized();
    Project = await Mediator.Send(new GetProjectDetail.Query { ProjectId = id
});
ListComment = await Mediator.Send(new ListProjectComment.Query { SearchTerm = search, PageIndex = p, PageSize = s, ProjectId = id });
if (ListComment != null)
{
    Project.ListComment = ListComment;
    await NestedComment(ListComment);
}
return Page();

        }
            ListComment = await getListComment(id, search, p, s);

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
