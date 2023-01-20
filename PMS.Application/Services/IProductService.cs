using System.Collections.Generic;
using WebApplication1.Models;

namespace PMS.Application.Services
{
    public interface IProductService
    {
        List<ProductViewModel> GetAll();

    }
}
