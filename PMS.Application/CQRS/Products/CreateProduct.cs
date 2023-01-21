using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace PMS.Application.CQRS.Products
{
    public class CreateProduct
    {
        public class Command : IRequest
        {
            public Product Product { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IProductService productService;

            public Handler(IProductService productService)
            {
                this.productService = productService;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                productService.Add(request.Product);
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
