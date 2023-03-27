using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Application.ViewModels
{
    public class ProjectUserViewModel
    {
        public ManageUser User { get; set; }
        public Project Project { get; set; }
        public string ImageProfile { get; set; }
    }
}
