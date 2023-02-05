using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.CQRS.Projects
{
    public class ListProject
    {
        public class Query : IRequest<PagedList<ProjectViewModel>>
        {
            public string SearchTerm { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }
        public class Handler : IRequestHandler<Query, PagedList<ProjectViewModel>>
        {
            private readonly IProjectService projectService;

            public Handler(IProjectService projectService)
            {
                this.projectService = projectService;
            }

            public Task<PagedList<ProjectViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(projectService.GetAllWithPagination(request.SearchTerm, request.PageIndex, request.PageSize));
            }
        }

    }
}
