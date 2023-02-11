using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.IRepositories;
using System;
using System.Collections.Generic;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository projectTaskRepository;

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository)
        {
            this.projectTaskRepository = projectTaskRepository;
        }

        public void Add(ProjectTaskViewModel project)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProjectTaskViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public PagedList<ProjectTaskViewModel> GetAllWithPagination(string keyword, int page, int pageSize)
        {
            var query = projectTaskRepository.FindAll();
            return PagedList<ProjectTaskViewModel>.ToPagedList(query.ProjectTo<ProjectTaskViewModel>(), page, pageSize);
        }

        public ProjectTaskViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ProjectTaskViewModel project)
        {
            throw new NotImplementedException();
        }
    }
}
