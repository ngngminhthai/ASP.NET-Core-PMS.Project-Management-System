using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Data.Entities;
using PMS.Data.IRepositories;
using PMS.Infrastructure.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly ManageAppDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork, ManageAppDbContext context)
        {
            this.projectRepository = projectRepository;
            this.unitOfWork = unitOfWork;
            this.context = context;

        }

        public void Add(Project project, string userName)
        {
            var author = context.Users.Where(u => u.Email == userName).FirstOrDefault();
            var newProject = new Project
            {
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate= project.EndDate,
                Creator = author,
                TagId = project.TagId,
            };

            projectRepository.Add(newProject);
            Save();
            var id = newProject.Id;
            List<KanbanColume> kanbanColumes= new List<KanbanColume>
            {
                new KanbanColume { 
                    NameColume= "To do",
                    ProjectId = id,
                },
                new KanbanColume {
                    NameColume= "Doing",
                    ProjectId = id,
                },
                new KanbanColume {
                    NameColume= "Done",
                    ProjectId = id,
                }
            };
            context.kanbanColumes.AddRange(kanbanColumes);
            context.SaveChanges();

        }

        //public void Delete(int id)
        //{
        //    projectRepository.Remove(id);
        //}

        public List<ProjectViewModel> GetAll()
        {
            return projectRepository.FindAll().ProjectTo<ProjectViewModel>().ToList();
        }

        public ProjectViewModel GetById(int id)
        {
            return Mapper.Map<ProjectViewModel>(projectRepository.FindById(id, p => p.Creator, p => p.Tag)); ;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Project Project)
        {
            //Loại bỏ thuộc tính DateCreated khỏi hành động update
            projectRepository.Update(Project);
            Save();
        }

        public PagedList<ProjectViewModel> GetAllWithPagination(string keyword, int page, int pageSize, string email, int[] tag, bool mine)
        {
            var query = projectRepository.FindAll();
            if (!mine)
            {
                query = query.Where(p => p.Creator.Email.Equals(email)
            || p.ProjectUsers.Any(pu => pu.User.Email.Equals(email)));

            }
            else
            {
                query = query.Where(p => p.Creator.Email.Equals(email));
            }
            var x = tag;

            if (tag.Length > 0)
            {
                query = query.Where(p => tag.Contains(p.Tag.Id));
            }
            if (keyword != null)
            {
                query = query.Where(p => p.Name.Contains(keyword));
            }
            return PagedList<ProjectViewModel>.ToPagedList(query.ProjectTo<ProjectViewModel>(), page, pageSize);
        }

        public void Delete(int id)
        {
            projectRepository.Remove(id);
            Save();
        }
    }
}
