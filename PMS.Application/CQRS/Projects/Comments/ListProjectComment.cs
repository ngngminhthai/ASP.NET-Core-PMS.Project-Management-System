using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.CQRS.Projects.Comments
{
    public class ListProjectComment
    {
        public class Query : IRequest<PagedList<ProjectCommentViewModel>>
        {
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int? ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedList<ProjectCommentViewModel>>
        {
            private readonly IProjectCommentService projectCommentService;

            public Handler(IProjectCommentService projectCommentService)
            {
                this.projectCommentService = projectCommentService;
            }

            public Task<PagedList<ProjectCommentViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(projectCommentService.GetAllWithPagination(request.PageIndex, request.PageSize, request.ProjectId));
            }
        }
    }
}
