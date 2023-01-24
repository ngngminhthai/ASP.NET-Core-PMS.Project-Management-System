using AutoMapper;
using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Data.IRepositories;
using PMS.Infrastructure.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.Entities;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class ProductService : IProductService

    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Product product)
        {
            productRepository.Add(product);
            Save();
        }

        public void Delete(int id)
        {
            productRepository.Remove(id);
        }

        public List<ProductViewModel> GetAll()
        {
            return productRepository.FindAll().ProjectTo<ProductViewModel>().ToList();
        }

        public ProductViewModel GetById(int id)
        {
            return Mapper.Map<ProductViewModel>(productRepository.FindById(id));
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            //Loại bỏ thuộc tính DateCreated khỏi hành động update
            productRepository.Update(product, "DateCreated");
        }

        PagedList<ProductViewModel> IProductService.GetAllWithPagination(string keyword, int page, int pageSize)
        {
            var query = productRepository.FindAll();
            return PagedList<ProductViewModel>.ToPagedList(query.ProjectTo<ProductViewModel>(), page, pageSize);
        }
    }
}
