using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Data.IRepositories;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace PMS.Application.Implementations
{
    public class ProductService : IProductService

    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<ProductViewModel> GetAll()
        {
            return productRepository.FindAll().ProjectTo<ProductViewModel>().ToList();
        }

    }
}
