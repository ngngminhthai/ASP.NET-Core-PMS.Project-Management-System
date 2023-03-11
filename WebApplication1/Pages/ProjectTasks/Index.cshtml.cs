using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PMS.Application.CQRS.ProjectTasks;
using PMS.Application.Implementations;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.Entities;
using PMS.DataEF.Repositories;
using PMS.Pages.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.ProjectTasks
{
    public class IndexModel : BasePageModel
    {
        private readonly IProjectTaskService projectTaskService;
        private readonly ManageAppDbContext _context;

        public IndexModel(IProjectTaskService projectTaskService, ManageAppDbContext context)
        {
            this.projectTaskService = projectTaskService;
            _context = context;
        }

        public PaginationParams paginationParams { get; set; } = new PaginationParams();

        

        public PagedList<ProjectTask> ProjectTask { get; set; }


        public int Id { get; set; }
        public List<KanbanColume> Kanbans { get; set; }

        public async Task OnGetAsync(int id, string search, int p = 1, int s = 3)
        {
            ProjectTask = GetAllWithPagination(id, search, p, s);
            Id = id;
            Kanbans = _context.kanbanColumes.Where(k => k.ProjectId == id).ToList();
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = ProjectTask.MetaData.TotalCount;
        }


        public IActionResult OnPostCreate(string name, int ProjectId, DateTime StartDate, DateTime EndDate, int KanbanId ,int PriorityValue, int WorkingStatusValue, string Description)
        {

            int length = projectTaskService.Count(ProjectId) +1;
            var newTask = new ProjectTask
            {
                Name = name,
                ProjectId = ProjectId,
                StartDate = StartDate,
                EndDate = EndDate,
                KanbanColumeID = KanbanId,
                Description = Description,
                PriorityValue = PriorityValue,
                WorkingStatusValue = WorkingStatusValue,
                Order = length,
            };
            projectTaskService.Add(newTask);

            return Redirect("../ProjectTasks?id=" + ProjectId);

            // return Page();
        }
        public IActionResult OnPostDelete(int projectTaskId, int id)
        {
           
            projectTaskService.Delete(projectTaskId);

            return Redirect("../ProjectTasks?id=" + id);

            
        }
        public PagedList<ProjectTask> GetAllWithPagination(int id, string keyword, int page, int pageSize)
        {
            var query = _context.ProjectTasks.Include(p => p.KanbanColume ).Include(p => p.Project).Where(p => p.ProjectId == id);
            return PagedList<ProjectTask>.ToPagedList(query , page, pageSize);
        }

    }
}
