using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
using WebApplication1.Models;

namespace PMS.Application.Services
{
    public interface IProductService
    {

        Task<List<ProductViewModel>> GetAllWithPagination(string keyword, int page, int pageSize);
        List<ProductViewModel> GetAll();
        void Update(Product product);
        void Delete(int id);
        ProductViewModel GetById(int id);

    }
}
