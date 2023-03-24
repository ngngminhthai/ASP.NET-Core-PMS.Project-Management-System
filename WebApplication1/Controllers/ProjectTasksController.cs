using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly ManageAppDbContext _context;

        public ProjectTasksController(ManageAppDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
            var manageAppDbContext = _context.ProjectTasks.Include(p => p.KanbanColume).Include(p => p.Project);
            return View(await manageAppDbContext.ToListAsync());
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks
                .Include(p => p.KanbanColume)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            ViewData["KanbanColumeID"] = new SelectList(_context.kanbanColumes, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ProjectId,KanbanColumeID,PriorityValue,WorkingStatusValue,StartDate,EndDate,Id")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KanbanColumeID"] = new SelectList(_context.kanbanColumes, "Id", "Id", projectTask.KanbanColumeID);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectTask.ProjectId);
            return View(projectTask);
        }

       
        [HttpPost]
        public async Task<string> Update(int id, int columeId, int order)
        {
            
            try
            {
                // update task 
                var task = _context.ProjectTasks.Where(p => p.Id == id).FirstOrDefault();

                var listOld = _context.ProjectTasks.Where(p => p.KanbanColumeID == task.KanbanColumeID).ToList();

                foreach (var item in listOld)
                {
                    if (task.Order < item.Order)
                    {
                        item.Order -= 1;
                    }
                }

                task.KanbanColumeID = columeId;
                task.Order = order;

             
                // update orther task
                var listTask = _context.ProjectTasks.Where(p=> p.KanbanColumeID == task.KanbanColumeID ).ToList();
                foreach (var item in listTask)
                {
                    if(task.Order <= item.Order)
                    {
                        item.Order += 1;
                    }
                }

                _context.Update(task);

                _context.SaveChanges();
                return "ok";
            }catch(Exception ex) { return "false"; };

        }
    
        [HttpPost]
        public async Task<string> ChangeOrder(int id, int order)
        {

            try
            {
                // update task 
                var task = _context.ProjectTasks.Where(p => p.Id == id).FirstOrDefault();
                
                

                // update orther task
                var listTask = _context.ProjectTasks.Where(p => p.KanbanColumeID == task.KanbanColumeID).ToList();
                
                
                foreach (var item in listTask)
                {
                    if (task.Order < order ) {
                        if( item.Order <= order && item.Order > task.Order)
                        {
                            item.Order -= 1;
                        }
                    }
                    else if(task.Order > order)
                    {
                        if(item.Order >= order && item.Order < task.Order)
                        {
                            item.Order += 1;
                        }
                    }
                    
                }
                task.Order = order;
                _context.Update(task);

                _context.SaveChanges();
                return "ok";
            }
            catch (Exception ex) { return "false"; };

        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks
                .Include(p => p.KanbanColume)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            _context.ProjectTasks.Remove(projectTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.Id == id);
        }
    }
}
