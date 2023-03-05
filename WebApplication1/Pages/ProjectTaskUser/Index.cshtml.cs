using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Implementations;
using PMS.Application.Services;
using PMS.Application.Services.ProjectTasks;
using PMS.Application.ViewModels;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.ProjectTaskUser
{
    public class IndexModel : PageModel
    {
        private readonly IProjectTask_UserService projectTask_User_Service;

        public IndexModel(IProjectTask_UserService projectTask_User_Service)
        {
            this.projectTask_User_Service = projectTask_User_Service;
        }
        public PaginationParams paginationParams { get; set; } = new PaginationParams();
        public PagedList<ProjectTask_User> ProjectTaskUser { get; set; }
        public int Id { get; set; }
        public bool? Flag { get; set; } = null;
        public string Error { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        public void SetFlagNull()
        {
            Flag = null;
        }
        public async void OnGetAsync(int id, string search, int p = 1, int s = 3)
        {
            ProjectTaskUser = projectTask_User_Service.GetAllUserInTask(id, search,p,s);
            Id = id;
            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = ProjectTaskUser.MetaData.TotalCount;
        }
        public async Task<IActionResult> OnPostAsync(int projectTaskId)
        {
            var result = projectTask_User_Service.Add(projectTaskId, UserName);
            if (result == 1)
            {
                Flag = false;
                Error = "Can not find User";
            }
            else if (result == 2)
            {
                Flag = false;
                Error = "User is exist";
            }
            else
            {
                Flag = true;
            }
            return Redirect("projectTaskUser?id="+projectTaskId);

        }
    }
}
