﻿using MediatR;
using PMS.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Application.CQRS.Projects.Comments
{
    public class DeleteProjectComment
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
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
                projectCommentService.Delete(request.Id);
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
