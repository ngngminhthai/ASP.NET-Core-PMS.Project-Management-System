using MediatR;
using PMS.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PMS.Application.CQRS.Products
{
    public class DeleteProduct
    {
        //public class Command : IRequest
        //{
        //    public int ProductId { get; set; }
        //}

        //public class Handler : IRequestHandler<Command>
        //{
        //    private readonly IProductService _productService;

        //    public Handler(IProductService productService)
        //    {
        //        _productService = productService;
        //    }

        //    public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        //    {
        //        _productService.Delete(request.ProductId);
        //        return Task.FromResult(Unit.Value);
        //    }
        //}
    }
}

