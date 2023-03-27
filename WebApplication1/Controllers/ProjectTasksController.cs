using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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


        [HttpPost]
        public void SubmitLists(string list1, string projectId)
        {
            int prjId = Convert.ToInt32(projectId);
            //var list = JsonConvert.DeserializeObject<List<dynamic>>(json);

            //var tasks = new List<dynamic>();

            //foreach (var item in list)
            //{
            //    var task = new
            //    {
            //        Id = (int)project.id,
            //        Title = project.title,
            //        DurationInDays = project.duration,
            //        Status = project.status
            //    };

            //    tasks.Add(task);
            //}
        
            dynamic value = JsonConvert.DeserializeObject(list1);

            DateTime forStartDate = value.start_date;
            DateTime forEndDate = value.end_date;


            int column = Convert.ToInt32(value.status);
            int length = _context.ProjectTasks.Where(k => k.KanbanColumeID == column).ToList().Count + 1;
            ProjectTask projectTask = new ProjectTask {

                ProjectId = prjId,
                StartDate = forStartDate.AddDays(1),
                EndDate = forEndDate.AddDays(1),
                Name = value.text,
                ParentId = value.parent,
                Description = value.title,
                Order = length,
                KanbanColumeID = Convert.ToInt32(value.status),
                PriorityValue = Convert.ToInt32(value.priority),
                WorkingStatusValue = 1,
            
            };

            _context.ProjectTasks.Add(projectTask);
            _context.SaveChanges();
           
        }
        [HttpPost]
        public void UpdateTask(string dataSend)
        {
            // int prjId = Convert.ToInt32(projectId);
            //var list = JsonConvert.DeserializeObject<List<dynamic>>(json);

            //var tasks = new List<dynamic>();

            //foreach (var item in list)
            //{
            //    var task = new
            //    {
            //        Id = (int)project.id,
            //        Title = project.title,
            //        DurationInDays = project.duration,
            //        Status = project.status
            //    };

            //    tasks.Add(task);
            //}

            dynamic value = JsonConvert.DeserializeObject(dataSend);
            int column = Convert.ToInt32(value.status);
            int id = Convert.ToInt32(value.id);
            DateTime forStartDate = value.start_date;
            DateTime forEndDate = value.end_date;
            ProjectTask pt = _context.ProjectTasks.Where(p => p.Id == id).FirstOrDefault();




            pt.StartDate = forStartDate.AddDays(1);
            pt.EndDate = forEndDate.AddDays(1);
            pt.Name = value.text;
            pt.ParentId = value.parent;
            pt.Description = value.title;
           // pt.Order = length;
            pt.KanbanColumeID = Convert.ToInt32(value.status);
            pt.PriorityValue = Convert.ToInt32(value.priority);
            pt.WorkingStatusValue = 1;

            

            _context.ProjectTasks.Update(pt);
            _context.SaveChanges();

        }
        [HttpPost]
        public void CreateDependecy(int id, int target)
        {
            var task = _context.ProjectTasks.Where(p => p.Id == target).FirstOrDefault();
            string update;
            if (task.Dependencies != null)
            {
                update = task.Dependencies.ToString()+",";
            }
            else update = "";
            

            task.Dependencies = update +id.ToString();
            _context.ProjectTasks.Update(task);
            _context.SaveChanges();

        }
        [HttpPost]
        public void DeleteTask(int dataSend)
        {


            var task = _context.ProjectTasks.Where(p => p.Id == dataSend).FirstOrDefault();
            _context.ProjectTasks.Remove(task);
            _context.SaveChanges();

        }
    }
}
