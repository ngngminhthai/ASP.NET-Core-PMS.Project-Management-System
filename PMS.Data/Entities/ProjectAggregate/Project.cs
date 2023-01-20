using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ManageUser Creator { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
