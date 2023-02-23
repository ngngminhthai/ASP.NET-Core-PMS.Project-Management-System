using PMS.Data.IRepositories.SystemRoles;
using WebApplication1.Data;
using WebApplication1.Data.Entities;

namespace PMS.DataEF.Repositories
{
    public class ProductRepository : EFRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
