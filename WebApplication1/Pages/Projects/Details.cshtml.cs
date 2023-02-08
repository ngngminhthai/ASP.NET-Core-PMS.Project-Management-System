﻿using Microsoft.EntityFrameworkCore;
using PMS.Pages.Shared;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace PMS.Pages.Projects
{
    public class DetailsModel : BasePageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public DetailsModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }
        public ProjectViewModel Project { get; set; }

        public async Task OnGetAsync(int id)
        {

            var project = _context.Projects.Include(x => x.Creator).FirstOrDefault(x => x.Id == id);
            Project = new ProjectViewModel { Id = project.Id, Description = project.Description, Name = project.Name, Email = project.Creator.Email };


        }
        //public async Task<ProjectViewModel> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return null;
        //    }
        //    var project = await Mediator.Send(new GetProjectDetail.Query { ProjectId = id });
        //    if (project == null)
        //    {
        //        return null;
        //    }
        //    var passedProduct = Mapper.Map<Project>(project);
        //    return passedProduct;
        //}
        //public async Task<ProjectViewModel> GetProjectDetail(int id)
        //{
        //    var result = await Mediator.Send(new GetProjectDetail.Query()
        //    {
        //        ProjectId = id,
        //    });

        //    return result;
        //}
    }
}