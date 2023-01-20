using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectComment
    {
        [Key]
        public int Id { get; set; }
        public Project Project { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
