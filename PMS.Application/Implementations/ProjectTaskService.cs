using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories.ProjectTasks;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.Entities.ProjectAggregate;
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

        public void Add(ProjectTask projectTask)
        {
            projectTaskRepository.Add(projectTask);
            Save();
        }

        public void Delete(int id)
        {
            projectTaskRepository.Remove(id);
            Save();
        }
        public int Count(int projectId)
        {
            int length = projectTaskRepository.FindAll().Where(p=> p.ProjectId == projectId ).Count();
            return length;
        }
        public List<ProjectTaskViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ProjectTask_User> GetAllProjecTaskUser()
        {
            throw new NotImplementedException();
        }

        public PagedList<ProjectTaskViewModel> GetAllWithPagination(int id, string keyword, int page, int pageSize)
        {
            var query = projectTaskRepository.FindAll().Where(p => p.Id == id);
            return PagedList<ProjectTaskViewModel>.ToPagedList(query.ProjectTo<ProjectTaskViewModel>(), page, pageSize);
        }

        public ProjectTaskViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProjectTaskViewModel> GetUpcommingTaskOfUser(string userid)
        {
            DateTime today = DateTime.Today;

            var closestTasks = projectTaskRepository.FindAll(p => p.ProjectTask_Users)
                .OrderBy(t => Math.Abs((t.StartDate - today).Days))
                .Take(4);



            return closestTasks.ProjectTo<ProjectTaskViewModel>().ToList();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(ProjectTaskViewModel project)
        {
            throw new NotImplementedException();
        }

        public void UpdatePriority(int id, int priorityValue)
        {
            var projectTask = projectTaskRepository.FindById(id);
            projectTask.PriorityValue = priorityValue;
            Save();
        }

        public void UpdateStatus(int id, int workStatus)
        {
            var projectTask = projectTaskRepository.FindById(id);
            projectTask.WorkingStatusValue = workStatus;
            Save();
        }
    }
}
