using Microsoft.AspNetCore.Mvc;
using PMS.Application.Services.ProjectTasks;
using PMS.Pages.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Pages
{
    public class DashboardModel : BasePageModel
    {
        private readonly IProjectTask_UserService projectTask_UserService;

        public DashboardModel(IProjectTask_UserService projectTask_UserService)
        {
            this.projectTask_UserService = projectTask_UserService;
        }

        [BindProperty]
        public List<ProjectTask> ProjectTasks { get; set; }

        public void OnGet()
        {
            var userName = User.Identity.Name;
            var today = DateTime.Today;
            var closestTasks = projectTask_UserService.GetAllTasksOfAllProjectsCurrentUserJoined(userName)
           .Where(tu => tu.ProjectTask.WorkingStatusValue == 2);

            var orderTasks = closestTasks.OrderBy(t => t.ProjectTask.EndDate).Select(t => t.ProjectTask).Take(4).ToList();

            ProjectTasks = orderTasks;
        }

        public string GetTaskClass(int index)
        {
            switch (index)
            {
                case 0:
                    return "bg-orange";
                case 1:
                    return "bg-yellow";
                case 2:
                    return "bg-blue";
                case 3:
                    return "bg-light";
                default:
                    return "bg-light";
            }
        }

    }
}
