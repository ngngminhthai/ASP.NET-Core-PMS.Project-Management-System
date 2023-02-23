using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Data.Entities.ProjectAggregate
{
    public class ProjectTask_User : DomainEntity<int>
    {
        public string UserId { get; set; }
        public int ProjectTaskId { get; set; }

        [ForeignKey("ProjectTaskId")]
        public ProjectTask ProjectTask { get; set; }
        [ForeignKey("UserId")]
        public ManageUser User { get; set; }
    }
}
