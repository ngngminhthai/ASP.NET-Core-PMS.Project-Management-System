using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Data.Entities.ProjectAggregate
{
    [Table("ProjectPermissions")]
    public class ProjectPermission : DomainEntity<int>
    {
        public int RoleId { get; set; }

        public string FunctionId { get; set; }

        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }

        [ForeignKey("RoleId")]
        public virtual ProjectRole ProjectRole { get; set; }

        [ForeignKey("FunctionId")]
        public virtual ProjectFunction ProjectFunction { get; set; }

    }
}
