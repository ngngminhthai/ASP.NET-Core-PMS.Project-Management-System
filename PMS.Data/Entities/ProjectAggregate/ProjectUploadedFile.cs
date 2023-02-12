using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Data.Entities.ProjectAggregate
{
    public class ProjectUploadedFile : DomainEntity<int>, IUpdateTimeStamp
    {
        public string File { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
