using PMS.Data.Entities;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class Project : DomainEntity<int>, IDateTimeStamp
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ManageUser Creator { get; set; }
        public int? TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag? Tag { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<ProjectUploadedFile> ProjectUploadedFiles { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ProjectComment> ProjectComments { get; set; }
        public ICollection<ProjectRole> ProjectRoles { get; set; }
        public ICollection<KanbanColume> kanbanColumes { get; set; }
    }
}
