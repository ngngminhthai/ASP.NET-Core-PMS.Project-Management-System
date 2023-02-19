using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;

namespace PMS.Application.CQRS.Projects.Comments
{
    public class CreateProjectComment
    {
        public class Command : IRequest
        {
            public ProjectComment ProjectComment { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IProjectCommentService projectCommentService;

            public Handler(IProjectCommentService projectCommentService)
            {
                this.projectCommentService = projectCommentService;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                projectCommentService.Add(request.ProjectComment);
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
