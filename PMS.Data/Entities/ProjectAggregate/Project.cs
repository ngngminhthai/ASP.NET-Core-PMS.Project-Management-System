using PMS.Data.Entities.ProjectAggregate;
using PMS.Infrastructure.SharedKernel;
using System.Collections.Generic;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class Project : DomainEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ManageUser Creator { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<ProjectUploadedFile> ProjectUploadedFiles { get; set; }
    }
}
