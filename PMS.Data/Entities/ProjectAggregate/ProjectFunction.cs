using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace PMS.Data.Entities.ProjectAggregate
{
    public class ProjectFunction : DomainEntity<string>
    {
        [Required]
        [StringLength(128)]
        public string Name { set; get; }

    }
}
