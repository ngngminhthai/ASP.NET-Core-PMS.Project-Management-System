using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace WebApplication1.Data.Entities.UserAggregate
{
    [Table("Permissions")]
    public class Permission : DomainEntity<int>
    {
        public string RoleId { get; set; }

        public string FunctionId { get; set; }

        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }


        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }
    }
}
