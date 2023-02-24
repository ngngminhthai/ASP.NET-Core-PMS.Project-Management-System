using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.Entities;
using PMS.Data.IRepositories;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork unitOfWork;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(TagViewModel tagVM)
        {
            var tag = new Tag
            {
                Id = tagVM.Id,
                TagName = tagVM.Name,
            };

            tagRepository.Add(tag);
            Save();
        }

        public List<TagViewModel> GetAll()
        {
            return tagRepository.FindAll().ProjectTo<TagViewModel>().ToList();
        }

        public PagedList<TagViewModel> GetAllWithPagination(string keyword, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public TagViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(TagViewModel tagVM)
        {
            throw new NotImplementedException();
        }
    }
}
