using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Products;
using PMS.Authorization;
using PMS.Pages.Shared;
using RBAC.Application.Authorization;
using System.Threading.Tasks;
using TeduCoreApp.Authorization;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly IAuthorizationService authorizationService;

        public IndexModel(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public PagedList<ProductViewModel> Product { get; set; }
        public PaginationParams paginationParams { get; set; } = new PaginationParams();
        public async Task<IActionResult> OnGetAsync(string? search, int p = 1, int s = 3)
        {
            var result = await authorizationService.AuthorizeAsync(User, new Payload { Resource = "PROJECT", ProjectRequirement = new ProjectRequirement() { ProjectId = 3, Action = "UPDATE", Resource = "TASK" } }, Operations.Update);
            if (result.Succeeded == false)
                return Unauthorized();

            Product = await GetProducts(search, p, s);

            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = Product.MetaData.TotalCount;
            return Page();
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
