using PMS.Infrastructure.SharedKernel;
using System;
namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectComment : DomainEntity<int>
    {
        public Project Project { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
