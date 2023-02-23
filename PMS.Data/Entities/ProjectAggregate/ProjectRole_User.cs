using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Entities;

namespace PMS.Data.Entities.ProjectAggregate
{
    public class ProjectRole_User : DomainEntity<int>
    {
        public string UserId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public ManageUser User { get; set; }

        [ForeignKey("RoleId")]
        public ProjectRole ProjectRole { get; set; }
    }
}
