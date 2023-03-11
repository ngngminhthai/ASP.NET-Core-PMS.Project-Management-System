using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Application.CQRS.Projects.Comments;
using PMS.Data.Entities;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;

namespace PMS.Pages.Kanban
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public IndexModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }


        public IList<KanbanColume> KanbanColumes { get;set; }
        public Project Project { get; set; }
        [BindProperty(SupportsGet =true)]
        public int projectId { get; set; }
        public async Task OnGetAsync()
        {
            var value = await _context.kanbanColumes
                .Include(k => k.project).Where(p => p.ProjectId == projectId).ToListAsync();

            Project = _context.Projects.Where(p => projectId == p.Id).FirstOrDefault();
            
            KanbanColumes = await SetProjectTaskForKanban(value);
        }
        public async Task<List<KanbanColume>> SetProjectTaskForKanban(List<KanbanColume> kanbanColumes)
        {

            if (kanbanColumes != null)
            {

                foreach (KanbanColume item in  kanbanColumes)
                {
                    item.projectTasks = await _context.ProjectTasks.Include(p => p.KanbanColume)
                        .Where(p => p.KanbanColumeID == item.Id)
                        .OrderBy(p=> p.Order).ToListAsync();
                     
                }
                return kanbanColumes;
            }
            return null;

        }
        public IActionResult OnPost(int projectId, string columnName)
        {
            KanbanColume kbc = new KanbanColume
            {
                ProjectId = projectId,
                NameColume = columnName
            };
            _context.kanbanColumes.Add(kbc);
            _context.SaveChanges();

            return Redirect("../Kanban?projectId=" + projectId);
        }
    }
}
