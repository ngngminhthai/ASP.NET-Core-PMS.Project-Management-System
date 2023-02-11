using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectTask : DomainEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }

        [NotMapped]
        public Priority Priority { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}
