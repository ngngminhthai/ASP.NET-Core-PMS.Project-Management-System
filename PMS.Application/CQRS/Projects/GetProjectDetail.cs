using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace PMS.Application.CQRS.Projects
{
    public class GetProjectDetail
    {
        public class Query : IRequest<ProjectViewModel>
        {
            public int? ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProjectViewModel>
        {
            private readonly IProjectService _projectService;

            public Handler(IProjectService projectService)
            {
                _projectService = projectService;
            }

            public Task<ProjectViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = _projectService.GetById((int)request.ProjectId);
                return Task.FromResult(product);
            }
        }
    }
}
