using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using PMS.Application.Implementations;
using PMS.Application.Services;
using PMS.Application.ViewModels;
using PMS.Data.Entities;
using System;
using System.Threading.Tasks;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.ProjectTasks
{
    public class IndexModel : PageModel
    {
        private readonly IProjectTaskService projectTaskService;
        public PaginationParams paginationParams { get; set; } = new PaginationParams();

        public IndexModel(IProjectTaskService projectTaskService)
        {
            this.projectTaskService = projectTaskService;
        }

        public PagedList<ProjectTaskViewModel> ProjectTask { get; set; }


        public int Id { get; set; }

        public async void OnGetAsync(int id, string search, int p = 1, int s = 3)
        {
            ProjectTask = projectTaskService.GetAllWithPagination(id, "", p, s);
            Id = id;
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = ProjectTask.MetaData.TotalCount;
        }
        public async Task<IActionResult> OnPostCreateAsync(string name, int ProjectId, DateTime StartDate, DateTime EndDate, int PriorityValue, int WorkingStatusValue, string Description)
        {
            var newTask = new ProjectTask
            {
                Name = name,
                ProjectId = ProjectId,
                StartDate = StartDate,
                EndDate = EndDate,
                Description = Description,
                PriorityValue = PriorityValue,
                WorkingStatusValue = WorkingStatusValue
            };
            projectTaskService.Add(newTask);

            return Redirect("../ProjectTasks?id=" + ProjectId);

            // return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int projectTaskId, int id)
        {
           
            projectTaskService.Delete(projectTaskId);

            return Redirect("../ProjectTasks?id=" + id);

            
        }

    }
}
