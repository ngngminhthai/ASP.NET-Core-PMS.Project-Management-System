using MediatR;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.RequestHelpers;

namespace PMS.Application.CQRS.Projects
{
    public class ListProjectUser
    {
        public class Query : IRequest<PagedList<ProjectUserViewModel>>
        {

            public string SearchTerm { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int ProjectId { get; set; }
        }
        public class Handler : IRequestHandler<Query, PagedList<ProjectUserViewModel>>
        {
            private readonly IProjectUserService projectUserService;

            public Handler(IProjectUserService projectUserService)
            {
                this.projectUserService = projectUserService;
            }

            public Task<PagedList<ProjectUserViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(projectUserService.GetAllUserInProject(request.ProjectId, request.SearchTerm, request.PageIndex, request.PageSize));

            }
        }
    }
}
