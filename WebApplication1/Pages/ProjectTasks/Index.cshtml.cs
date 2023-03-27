using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Data.Entities;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Pages.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;
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
        public List<ManageUser> Users { get; set; }

        //biding
        public int Priority { get; set; }
        public int KanbanId { get; set; }
        public string Search { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string [] Member { get; set; }



        public async Task OnGetAsync(int id, string search, string from,
            string to, string[] members
           , int kanbanId = 0, int priority = 0, int p = 1, int s = 6)
        {
            IQueryable<ProjectTask> task = _context.ProjectTasks.Include(p => p.ProjectTask_Users)
                        .Include(p => p.KanbanColume);
                
            Id = id;
            Priority = priority;
            
            if( from !=null)
            {
                DateTime dateFrom = DateTime.ParseExact(from, "dd-MM-yyyy", CultureInfo.InvariantCulture);
              
                task =task.Where(t => t.StartDate >= dateFrom);
                From = dateFrom;
            }
            if (to != null)
            {
                DateTime dateTo= DateTime.ParseExact(to, "ddMM-yyyy", CultureInfo.InvariantCulture);
                task = task.Where(t => t.EndDate <= dateTo);
                To = dateTo;
            }
            if(search != null)
            {
                task = task.Where(t => t.Name.Contains(search));
                Search = search;
            }
            if (members.Length > 0 )
            {
                // int[] value = new int [];
                Member = members;
            
           


                task = task.Where(t => t.ProjectTask_Users.Any(p => members.Contains(p.UserId)));
            }
           
            if(kanbanId != 0)
            {
                task = task.Where(t => t.KanbanColumeID == kanbanId);
                KanbanId = kanbanId;
            }
            if (priority != 0)
            {
                task = task.Where(t => t.PriorityValue == priority);
                Priority = priority;
            }
            var users = _context.ProjectUsers.Include(p => p.User)
                .Where(p => p.ProjectId == id).Select(u => u.User).ToList();


            var creator = _context.Projects.Include(p => p.Creator).Where(p => p.Id == id)
                .Select(p => p.Creator).FirstOrDefault();
            users.Add(creator);
            Users = users.ToList();
            Kanbans = _context.kanbanColumes.Where(c => c.ProjectId == id).ToList();
            ProjectTask = PagedList<ProjectTask>.ToPagedList(task,p, s);
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = ProjectTask.MetaData.TotalCount;
       


        }

        public string ConvetPriority(int id)
        {
            if (id == 1) return "low";
            if (id == 2) return "medium";
            if (id == 3) return "high";
            return "high";
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
