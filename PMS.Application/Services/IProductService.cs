using System.Collections.Generic;
using WebApplication1.Data.Entities;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface IProductService
    {
        void Add(Product product);
        PagedList<ProductViewModel> GetAllWithPagination(string keyword, int page, int pageSize);
        List<ProductViewModel> GetAll();
        void Update(Product product);
        void Delete(int id);
        ProductViewModel GetById(int id);
        void Save();

    }
}
