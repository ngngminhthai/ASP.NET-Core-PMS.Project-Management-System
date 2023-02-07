using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Data.Entities.ProjectAggregate
{
    public class ProjectUploadedFile : DomainEntity<int>
    {
        public string File { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}
