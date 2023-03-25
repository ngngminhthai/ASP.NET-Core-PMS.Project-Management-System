using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.Entities;
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



        public PagedList<ProjectTaskViewModel> ProjectTask { get; set; }



        public int Id { get; set; }
        public List<KanbanColume> Kanbans { get; set; }

        public async Task OnGetAsync(int id, string search, int p = 1, int s = 6)
        {
            ProjectTask = projectTaskService.GetAllWithPagination(id, null, p, s);
            Id = id;
            Kanbans = _context.kanbanColumes.Where(c => c.ProjectId == id).ToList();
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = ProjectTask.MetaData.TotalCount;



        }


        public IActionResult OnPostCreate(string name, int ProjectId, DateTime StartDate, DateTime EndDate, int KanbanId, int PriorityValue, int WorkingStatusValue, string Description)
        {
            int length = _context.ProjectTasks.Where(k => k.KanbanColumeID == KanbanId).ToList().Count + 1;
            //int length = projectTaskService.Count(ProjectId) + 1;
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
       


        //public PagedList<ProjectTask> OnPostUpdate(int id, string keyword, int page, int pageSize)
        //{
        //    var query = _context.ProjectTasks.Include(p => p.KanbanColume).Include(p => p.Project).Where(p => p.ProjectId == id);
        //    return PagedList<ProjectTask>.ToPagedList(query, page, pageSize);
        //}
      
        public IActionResult OnPostUpdate2(int projectTaskId, int columnId, int projectId, int page)
        {


            // update task 
            var task = _context.ProjectTasks.Where(p => p.Id == projectTaskId).FirstOrDefault();

            var listOld = _context.ProjectTasks.Where(p => p.KanbanColumeID == task.KanbanColumeID).ToList();

            int length = _context.ProjectTasks.Where(k => k.KanbanColumeID == columnId).ToList().Count();

            foreach (var item in listOld)
            {
                if (task.Order < item.Order)
                {
                    item.Order -= 1;
                }
            }

            task.KanbanColumeID = columnId;
            task.Order = length + 1;


            // update orther tas

            _context.Update(task);

            _context.SaveChanges();
            return Redirect("../ProjectTasks?id=" + projectId+"&p="+page);

        }
    }
   

}
