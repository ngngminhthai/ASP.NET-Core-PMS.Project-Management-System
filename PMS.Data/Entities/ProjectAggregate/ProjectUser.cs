using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectUser : DomainEntity<int>
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ManageUser User { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

    }
}
