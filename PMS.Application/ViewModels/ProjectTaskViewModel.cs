using PMS.Data.Entities.ValueObjects;
using System;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Application.ViewModels
{
    public class ProjectTaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Priority Priority { get; set; }
        public Project Project { get; set; }
        public WorkingStatus WorkingStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
