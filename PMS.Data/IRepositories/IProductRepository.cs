using PMS.Infrastructure.SharedKernel;
using WebApplication1.Data.Entities;

namespace PMS.Data.IRepositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
    }
}
