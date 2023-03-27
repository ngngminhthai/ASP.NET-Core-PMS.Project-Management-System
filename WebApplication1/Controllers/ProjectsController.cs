using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PMS.Application.CQRS.Projects;
using PMS.Application.CQRS.Projects.Comments;
using PMS.Application.Services;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Hubs;
using WebApplication1.Models;

namespace PMS.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly ManageAppDbContext _context;
        private readonly IHubContext<SignalSever> _signalrHub;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMemoryCache cache;
        private readonly IProjectService projectService;
        public ProjectsController(ManageAppDbContext context, IHubContext<SignalSever> signalrHub,
           IFileUploadService fileUploadService, IMemoryCache cache, IProjectService projectService)
        {
            _context = context;
            _signalrHub = signalrHub;
            this._fileUploadService = fileUploadService;
            this.cache = cache;
            this.projectService = projectService;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }

        /// <summary>
        /// This method used to test result with CRQR Mediator
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult GetProjects(string searchTerm, int page, int pageSize)
        {

            var projects = Mediator.Send(new ListProject.Query()
            {
                SearchTerm = searchTerm,
                PageIndex = page,
                PageSize = pageSize
            });

            Response.AddPaginationHeader(projects.Result.MetaData);
            return Ok(projects.Result);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> GetProjectAndComment(int id, int page = 1, int pageSize = 5)
        {


            var project = await Mediator.Send(new GetProjectDetail.Query { ProjectId = id });
            if (project == null)
            {
                return NotFound();
            }

            project.ListComment = await getListComment(id, page, pageSize);

            return Ok(project);
        }


        #region comment
        public async Task<List<ProjectCommentViewModel>> getListComment(int id, int page, int pageSize)
        {
            List<ProjectCommentViewModel> listCmt = await Mediator.Send(new ListProjectComment.Query { PageIndex = page, PageSize = pageSize, ProjectId = id });

            await NestedComment(listCmt);

            return listCmt.OrderByDescending(p => p.DateModified).ThenByDescending(p => p.DateCreated).ToList();
        }
        public async Task<List<ProjectCommentViewModel>> NestedComment(List<ProjectCommentViewModel> listChildComment)
        {

            if (listChildComment != null)
            {

                foreach (ProjectCommentViewModel comment in listChildComment)
                {
                    var list = await Mediator.Send(new GetListChildComment.Query { ParentId = comment.Id });
                    comment.ChildComments = await NestedComment(list);
                }
                return listChildComment.OrderByDescending(p => p.DateModified).ThenByDescending(p => p.DateCreated).ToList();
            }
            return null;

        }

        [HttpPost]
        public async void CreateComment(ProjectCommentViewModel comment)
        {
            string email = HttpContext.User.Identity.Name;
            var author = _context.Users.Where(u => u.Email == email).FirstOrDefault();

            var projectComent = _context.Projects.Where(p => p.Id == comment.ProjectID).FirstOrDefault();

            var newComment = new ProjectComment
            {
                Author = author,
                Content = comment.Content,
                Project = projectComent,
                ParentID = comment.ParentID,
                level = comment.Level,
                NumberOfLike = 0
            };
            await Mediator.Send(new CreateProjectComment.Command { ProjectComment = newComment });
            await _signalrHub.Clients.All.SendAsync("LoadProjectComment");

        }
        [HttpPost]
        public async void DeleteComment(int id)
        {
            string email = HttpContext.User.Identity.Name;
            var author = _context.Users.Where(u => u.Email == email).FirstOrDefault();

            var cmt = await Mediator.Send(new GetProjectCommentById.Query { Id = id });
            if (author.Id == cmt.Author.Id)
            {
                //  NestedDeleteComment(id);
                await Mediator.Send(new DeleteProjectComment.Command { Id = id });
            }
            await _signalrHub.Clients.All.SendAsync("LoadProjectComment");

        }

        public async void nesteddeletecomment(int id)
        {
            var list = await Mediator.Send(new GetListChildComment.Query { ParentId = id });

            if (list != null)
            {

                foreach (ProjectCommentViewModel comment in list)
                {

                    nesteddeletecomment(comment.Id);
                }

            }
            await Mediator.Send(new DeleteProjectComment.Command { Id = id });


        }



        [HttpPost]
        public async void UpdateComment(int id, string content)
        {
            string email = HttpContext.User.Identity.Name;
            var author = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            var cmt = await Mediator.Send(new GetProjectCommentById.Query { Id = id });
            if (author.Id == cmt.Author.Id)
            {
                await Mediator.Send(new UpdateProjectComment.Command { Id = id, Content = content });
            }

            await _signalrHub.Clients.All.SendAsync("LoadProjectComment");

        }

        #endregion
        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Project project)
        {
            //var email = HttpContext.User.Identity.Name;

            //projectService.Add(ProjectInput, email);

            return View();

        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        public List<ProjectRole> GetProjectRoles(int projectId, string userId)
        {
            List<ProjectRole> projectRoles = _context.ProjectRole_Users
                .Include(pr => pr.ProjectRole)
                .Where(pr => pr.UserId == userId)
                .Select(pr => pr.ProjectRole)
                .Where(r => r.ProjectId == projectId)
                .ToList();

            return projectRoles;
        }

        public IActionResult UpdateProjectRoles(List<ProjectRole_User> projectRoles, string userId, int projectId)
        {
            List<ProjectRole_User> projectRoles2 = _context.ProjectRole_Users
                .Include(pru => pru.ProjectRole)
                .Where(pru => pru.ProjectRole.ProjectId == projectId && pru.UserId == userId)
                .ToList();

            _context.RemoveRange(projectRoles2);
            _context.AddRange(projectRoles);
            _context.SaveChanges();
            return Redirect($"/ProjectUser/Index?projectId={projectId}");

        }

    }
}
