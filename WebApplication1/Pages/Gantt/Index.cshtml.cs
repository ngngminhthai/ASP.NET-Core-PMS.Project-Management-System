using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Pages.Gantt
{
	public class IndexModel : PageModel
	{
		private readonly WebApplication1.Data.ManageAppDbContext _context;

		public IndexModel(WebApplication1.Data.ManageAppDbContext context)
		{
			_context = context;
		}

		public IList<ProjectTask> ProjectTasks { get; set; }

		public async Task OnGetAsync()
		{
			ProjectTasks = await _context.ProjectTasks
				.Include(p => p.KanbanColume)
				.Include(p => p.Project).ToListAsync();

			foreach (var ProjectTask in ProjectTasks)
			{
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

					}

				}
			}

		}
	}
}
