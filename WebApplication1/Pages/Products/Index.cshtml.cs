using PMS.Application.Products;
using PMS.Pages.Shared;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Pages
{
    public class IndexModel : BasePageModel
    {
        public PagedList<ProductViewModel> Product { get; set; }
        public PaginationParams paginationParams { get; set; } = new PaginationParams();
        public async Task OnGetAsync(string? search, int p = 1, int s = 3)
        {
            Product = await GetProducts(search, p, s);

            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = Product.MetaData.TotalCount;
        }
        public async Task<PagedList<ProductViewModel>> GetProducts(string? searchTerm, int page, int pageSize)
        {
            var result = await Mediator.Send(new ListProduct.Query()
            {
                SearchTerm = searchTerm,
                PageIndex = page,
                PageSize = pageSize
            });
            return result;
        }

    }
}
