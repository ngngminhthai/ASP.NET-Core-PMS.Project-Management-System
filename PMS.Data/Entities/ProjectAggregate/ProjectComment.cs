using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectComment : DomainEntity<int>, IUpdateTimeStamp
    {
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }
        [ForeignKey("UserId")]
        public ManageUser Author { get; set; }
        public int NumberOfLike { get; set; }
        public string Content { get; set; }

        public int level { get; set; }
        public int? ParentID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        // public ICollection<ProjectComment>? ChildComments { get; set; }


    }
}
