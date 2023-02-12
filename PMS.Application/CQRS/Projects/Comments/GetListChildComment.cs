using MediatR;
using PMS.Application.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace PMS.Application.CQRS.Projects.Comments
{
    public class GetListChildComment
    {
        public class Query : IRequest<List<ProjectCommentViewModel>>
        {
            public int ParentId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<ProjectCommentViewModel>>
        {
            private readonly IProjectCommentService projectCommentService;

            public Handler(IProjectCommentService projectCommentService)
            {
                this.projectCommentService = projectCommentService;
            }

            public Task<List<ProjectCommentViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(projectCommentService.GetChildComments(request.ParentId));
            }
        }
    }
}
