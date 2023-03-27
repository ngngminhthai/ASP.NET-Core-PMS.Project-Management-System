using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Data.Entities;
using PMS.Data.Entities.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Pages.ProjectTasks
{
    public class EditModel : PageModel
    {
        private readonly IProjectTaskService projectTaskService;
        private readonly ManageAppDbContext _context;

        public EditModel(IProjectTaskService projectTaskService, ManageAppDbContext context)
        {
            this.projectTaskService = projectTaskService;
            _context = context;
        }

        [BindProperty]
        public ProjectTask ProjectTask { get; set; }
        public List<ManageUser> Assignees { get; set; }

        public List<ManageUser> Users { get; set; }

        public List<ProjectTask_User> ProjectTask_Users { get; set; }

        public List<ProjectTask> ProjectTasks { get; set; }

        public List<ProjectTask> Dependencies { get; set; } = new List<ProjectTask>();

        public List<KanbanColume> KanbanColumes { get; set; }

        public int KanbanColumeId { get; set; }

        public int PriorityValue { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectTask = await _context.ProjectTasks
                .Include(p => p.KanbanColume)
                .Include(p => p.Project).Include(p => p.ProjectTask_Users)
                .FirstOrDefaultAsync(m => m.Id == id);

            var assignees = _context.projectTask_Users
                .Where(ptu => ptu.ProjectTaskId == id)
                .Include(ptu => ptu.User)
                .ThenInclude(u => u.ProjectRole_Users)
                .ThenInclude(pru => pru.ProjectRole)
                .Select(u => new ManageUser
                {
                    UserName = u.User.UserName,
                    Role = string.Join(",", u.User.ProjectRole_Users.Select(pru => pru.ProjectRole.Name))
                })
                .ToList();


            Assignees = assignees;

            Users = _context.ProjectUsers.Where(u => u.ProjectId == ProjectTask.ProjectId).Include(pu => pu.User).Select(pu => pu.User).ToList();

            ProjectTask_Users = _context.projectTask_Users.Where(u => u.ProjectTaskId == ProjectTask.Id).ToList();

            ProjectTasks = _context.ProjectTasks.Where(t => t.ProjectId == ProjectTask.ProjectId).ToList();

            KanbanColumes = _context.kanbanColumes.Where(k => k.ProjectId == ProjectTask.ProjectId).ToList();

            KanbanColumeId = (int)ProjectTask.KanbanColumeID;

            PriorityValue = (int)ProjectTask.PriorityValue;

            if (ProjectTask != null)
            {
                if (!string.IsNullOrEmpty(ProjectTask.Dependencies))
                {
                    // Split the dependencies string into an array of task IDs
                    string[] dependencyIds = ProjectTask.Dependencies.Split(',');

                    // Parse the task IDs into integers
                    int[] dependencyIntIds = dependencyIds.Select(int.Parse).ToArray();

                    // Retrieve the dependent tasks from the database
                    IQueryable<ProjectTask> dependentTasks = _context.ProjectTasks.Where(t => dependencyIntIds.Contains(t.Id));

                    // Store the dependent tasks in a variable for later use
                    List<ProjectTask> taskDependencies = dependentTasks.ToList();

                    ProjectTask.DependentTasks = taskDependencies;
                    Dependencies = taskDependencies;
                }
            }
            else Dependencies = new List<ProjectTask>();


            if (ProjectTask == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnGetUpdateStatus(int id, int workStatus, int pid)
        {
            projectTaskService.UpdateStatus(id, workStatus);
            return RedirectToPage("/ProjectTasks/Index", new { id = pid });
        }

        public IActionResult OnGetUpdatePriority(int id, int priority, int pid)
        {
            projectTaskService.UpdatePriority(id, priority);
            return RedirectToPage("/ProjectTasks/Index", new { id = pid });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int taskId, string taskName, int kanbanColumn, int priority, DateTime startDate, DateTime endDate, int duration, string[] selectedUsers, string[] selectedDependencies, string taskDescription)
        {
            // Retrieve the existing task from the database
            var task = await _context.ProjectTasks.FindAsync(taskId);
            var taskUser = _context.projectTask_Users.Where(p => p.ProjectTaskId == taskId).ToList();
            if (taskUser != null || taskUser.Count() > 0)
            {
                List<ProjectTask_User> listUser = new List<ProjectTask_User>();
                foreach (var item in selectedUsers)
                {
                    var iscontain = taskUser.Where(p => p.UserId == item).FirstOrDefault();
                    if (iscontain == null)
                    {
                        taskUser.Add(new ProjectTask_User
                        {
                            ProjectTaskId = taskId,
                            UserId = item

                        }); ;
                    }
                }
                List<ProjectTask_User> listDelete = taskUser.Where(p => !selectedUsers.Contains(p.UserId)).ToList();

                foreach (var item in listDelete)
                {
                    taskUser.Remove(item);
                }

            }
            else
            {
                foreach (var item in selectedUsers)
                {
                   
                        taskUser.Add(new ProjectTask_User
                        {
                            ProjectTaskId = taskId,
                            UserId = item

                        }); ;
                    
                }
            }
            //taskUser.Remov(listDelete);

            if (task == null)
            {
                return NotFound();
            }

            // Update the task properties
            task.Name = taskName;
            task.KanbanColumeID = kanbanColumn;
            task.PriorityValue = priority;
            task.StartDate = startDate;
            task.EndDate = endDate;
            task.Description = taskDescription;
            

            // Update the task assignees
            var existingAssignees = await _context.projectTask_Users.Where(ptu => ptu.ProjectTaskId == taskId).ToListAsync();

            _context.RemoveRange(existingAssignees);

            foreach (var userId in selectedUsers)
            {
                var assignee = new ProjectTask_User { UserId = userId, ProjectTaskId = taskId };

                _context.Add(assignee);
            }

            // Update the task dependencies
            task.Dependencies = string.Join(",", selectedDependencies);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Redirect($"/ProjectTasks/edit?id={taskId}");
        }


        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.Id == id);
        }
    }
}
