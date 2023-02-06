using AutoMapper;
using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Data.IRepositories;
using PMS.Infrastructure.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class ProductService : IProductService

    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ManageAppDbContext context;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ManageAppDbContext context)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public void Add(ProductViewModel productVm)
        {
            var product = Mapper.Map<ProductViewModel, Product>(productVm);
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

        public void Update(ProductViewModel productVm)
        {
            var product = context.Products.Find(3);
            product.TestProperty = "123";

            var product2 = context.Products.Find(2);
            product2.TestProperty = "abcdef";
            Save();
            /* var product = Mapper.Map<ProductViewModel, Product>(productVm);
             //Loại bỏ thuộc tính DateCreated khỏi hành động update
             productRepository.Update(product, "DateCreated");
             Save();*/
        }

        PagedList<ProductViewModel> IProductService.GetAllWithPagination(string keyword, int page, int pageSize)
        {
            var query = productRepository.FindAll();
            return PagedList<ProductViewModel>.ToPagedList(query.ProjectTo<ProductViewModel>(), page, pageSize);
        }
    }
}
