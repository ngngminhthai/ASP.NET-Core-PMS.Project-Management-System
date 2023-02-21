using AutoMapper;
using AutoMapper.QueryableExtensions;
using PMS.Application.Services;
using PMS.Data.IRepositories;
using PMS.DataEF.Repositories;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class ProjectCommentService : IProjectCommentService
    {
        private readonly IProjectCommentRepository projectCommentRepository;
        private readonly IUnitOfWork unitOfWork;
        public ProjectCommentService(IProjectCommentRepository projectCommentRepository, IUnitOfWork unitOfWork)
        {
            this.projectCommentRepository = projectCommentRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Add(ProjectComment comment)
        {
            
            projectCommentRepository.Add(comment);
            Save();
        }

        public void Delete(int id)
        {
            projectCommentRepository.Remove(id);
            Save();
        }

        public List<ProjectCommentViewModel> GetAll()
        {
            return projectCommentRepository.FindAll().ProjectTo<ProjectCommentViewModel>().ToList();
        }

        public List<ProjectCommentViewModel> GetChildComments(int parentId)
        {
            var query = projectCommentRepository.FindAll().Where(pm => pm.ParentID == parentId);

            return query.ProjectTo<ProjectCommentViewModel>().ToList();
        }

        public PagedList<ProjectCommentViewModel> GetAllWithPagination(int page, int pageSize, int? projectId)
        {
            var query = projectCommentRepository.FindAll();
            if (projectId != null)
            {
                query = query.Where(pm => pm.Project.Id == projectId);
            }
            return PagedList<ProjectCommentViewModel>.ToPagedList(query.ProjectTo<ProjectCommentViewModel>(), page, pageSize);
        }

        public ProjectCommentViewModel GetById(int id)
        {
            return Mapper.Map<ProjectCommentViewModel>(projectCommentRepository.FindById(id));
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update( int id, string content )
        {
            var projectComment = projectCommentRepository.FindById(id);

            projectComment.Content = content;
            Save();
        }

      

        public ProjectComment GetCommentById(int id)
        {
            return projectCommentRepository.FindById(id);
        }
    }
}
