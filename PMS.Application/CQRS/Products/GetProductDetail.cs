using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace PMS.Application.CQRS.Products
{
    public class GetProductDetail
    {
        public class Query : IRequest<ProductViewModel>
        {
            public int? ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProductViewModel>
        {
            private readonly IProductService _productService;

            public Handler(IProductService productService)
            {
                _productService = productService;
            }

            public Task<ProductViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = _productService.GetById((int)request.ProductId);
                return Task.FromResult(product);
            }
        }
    }
}
