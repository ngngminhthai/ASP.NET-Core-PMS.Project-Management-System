using MediatR;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PMS.Application.CQRS.Tags
{
    public class ListTag
    {

        public class Query : IRequest<List<TagViewModel>>
        {

        }
        public class Handler : IRequestHandler<Query, List<TagViewModel>>
        {
            private readonly ITagService tagService;

            public Handler(ITagService tagService)
            {
                this.tagService = tagService;
            }

            public Task<List<TagViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {

                return Task.FromResult(tagService.GetAll());
            }
        }
    }
}
