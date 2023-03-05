using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PMS.Application.CQRS.Projects
{
    public class AddProjectUser
    {
        public class Command : IRequest
        {
            public int ProjectID { get; set; }
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IProjectUserService projectUserService;

            public Handler(IProjectUserService projectUserService)
            {
                this.projectUserService = projectUserService;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                projectUserService.Add(request.ProjectID, request.UserName);
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
