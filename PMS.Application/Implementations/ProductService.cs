﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Data.IRepositories;
using PMS.Infrastructure.Extensions;
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

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Add(Product product)
        {
            productRepository.Add(product);
        }

        public void Delete(int id)
        {
            productRepository.Remove(id);
        }

        public List<ProductViewModel> GetAll()
        {
            return productRepository.FindAll().ProjectTo<ProductViewModel>().ToList();
        }

        public List<ProductViewModel> GetAllWithPagination(string keyword, int page, int pageSize)
        {
            var query = productRepository.FindAll().Search(keyword, "Name");
            return PagedList<ProductViewModel>.ToPagedList(query.ProjectTo<ProductViewModel>(), page, pageSize);
        }

        public ProductViewModel GetById(int id)
        {
            return Mapper.Map<ProductViewModel>(productRepository.FindById(id));
        }

        public void Update(Product product)
        {
            //Loại bỏ thuộc tính DateCreated khỏi hành động update
            productRepository.Update(product, "DateCreated");
        }
    }
}
