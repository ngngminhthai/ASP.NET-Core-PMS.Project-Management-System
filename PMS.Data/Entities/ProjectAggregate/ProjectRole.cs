using PMS.Infrastructure.SharedKernel;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Data.Entities.ProjectAggregate
{
    public class ProjectRole : DomainEntity<int>
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
