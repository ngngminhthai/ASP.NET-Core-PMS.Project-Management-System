using MediatR;
using PMS.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;

namespace PMS.Application.CQRS.Projects.Comments
{
    public class GetProjectCommentById
    {
        public class Query : IRequest<ProjectComment>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProjectComment>
        {
            private readonly IProjectCommentService projectCommentService;

            public Handler(IProjectCommentService projectCommentService)
            {
                this.projectCommentService = projectCommentService;
            }

            public Task<ProjectComment> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(projectCommentService.GetCommentById(request.Id));
            }
        }
    }
}
