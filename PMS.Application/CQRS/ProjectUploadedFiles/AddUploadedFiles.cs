using MediatR;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace PMS.Application.CQRS.ProjectUploadedFiles
{
    public class AddUploadedFiles
    {
        public class Command : IRequest
        {
            public ProjectUploadedFileViewModel projectUploadedFileViewModel { get; set; }
            public int ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {

            public Handler(IProjectUploadedFileService projectUploadedFileService)
            {
                ProjectUploadedFileService = projectUploadedFileService;
            }

            public IProjectUploadedFileService ProjectUploadedFileService { get; }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                ProjectUploadedFileService.Add(request.ProjectId, request.projectUploadedFileViewModel);
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
