using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace PMS.Pages.Shared
{
    [Authorize(Roles = "Admin, User")]
    public class BasePageModel : PageModel
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices
            .GetService<IMediator>();
    }
}
