using MediatR;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.RequestHelpers;

namespace PMS.Application.CQRS.ProjectTasks
{
    public class ListProjectTask
    {
        public class Query : IRequest<PagedList<ProjectTaskViewModel>>
        {

            public string SearchTerm { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int ProjectId { get; set; }
        }
        public class Handler : IRequestHandler<Query, PagedList<ProjectTaskViewModel>>
        {
            private readonly IProjectTaskService projectTaskService;

            public Handler(IProjectTaskService projectTaskService)
            {
                this.projectTaskService = projectTaskService;
            }

            public Task<PagedList<ProjectTaskViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(projectTaskService.GetAllWithPagination(request.ProjectId, request.SearchTerm, request.PageIndex, request.PageSize));

            }
        }
    }
}
