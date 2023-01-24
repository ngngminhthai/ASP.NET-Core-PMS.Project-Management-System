using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Products
{
    public class ListProduct
    {
        public class Query : IRequest<PagedList<ProductViewModel>>
        {
            public string SearchTerm { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedList<ProductViewModel>>
        {
            private readonly IProductService productService;

            public Handler(IProductService productService)
            {
                this.productService = productService;
            }

            public Task<PagedList<ProductViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(productService.GetAllWithPagination(request.SearchTerm, request.PageIndex, request.PageSize));
            }
        }
    }
}