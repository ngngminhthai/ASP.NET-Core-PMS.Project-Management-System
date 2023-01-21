using MediatR;
using PMS.Application.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace PMS.Application.Products
{
    public class List
    {
        public class Query : IRequest<List<ProductViewModel>> { }

        public class Handler : IRequestHandler<Query, List<ProductViewModel>>
        {
            private readonly IProductService productService;

            public Handler(IProductService productService)
            {
                this.productService = productService;
            }

            public async Task<List<ProductViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await productService.GetAllWithPagination("2", 2, 3);
            }
        }
    }
}