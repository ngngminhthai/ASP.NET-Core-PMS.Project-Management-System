using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.IRepositories;
using PMS.Infrastructure.SharedKernel;
using System;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class ProjectUploadedFileService : IProjectUploadedFileService
    {
        private readonly IProjectUploadedFileRepository projectUploadedFileRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectRepository projectRepository;

        public ProjectUploadedFileService(IProjectUploadedFileRepository projectUploadedFileRepository,
            IUnitOfWork unitOfWork,
            IProjectRepository projectRepository
            )
        {
            this.projectUploadedFileRepository = projectUploadedFileRepository;
            this.unitOfWork = unitOfWork;
            this.projectRepository = projectRepository;
        }

        public void Add(int ProjectId, ProjectUploadedFileViewModel projectUploadedFileViewModel)
        {
            projectUploadedFileRepository.Add(new ProjectUploadedFile { File = projectUploadedFileViewModel.File, Project = projectRepository.FindById(ProjectId) });
            unitOfWork.Commit();
        }

        public PagedList<ProjectViewModel> GetAllWithPagination(string keyword, int page, int pageSize, string email)
        {
            throw new NotImplementedException();
        }
    }
}
