using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.IRepositories;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository projectTaskRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IUnitOfWork unitOfWork)
        {
            this.projectTaskRepository = projectTaskRepository;
            this.unitOfWork = unitOfWork;
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

        public PagedList<ProjectTaskViewModel> GetAllWithPagination(int id, string keyword, int page, int pageSize)
        {
            var query = projectTaskRepository.FindAll(p => p.ProjectId == id, p => p.Project);
            return PagedList<ProjectTaskViewModel>.ToPagedList(query.ProjectTo<ProjectTaskViewModel>(), page, pageSize);
        }

        public ProjectTaskViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(ProjectTaskViewModel project)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(int id, int workStatus)
        {
            var projectTask = projectTaskRepository.FindById(id);
            projectTask.WorkingStatusValue = workStatus;
            Save();
        }
    }
}
