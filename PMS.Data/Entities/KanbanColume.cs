using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.Data.Entities
{
    public class KanbanColume : DomainEntity<int>
    {
        public string NameColume { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? project { get; set; }

        public ICollection<ProjectTask> projectTasks { get; set; }
    }
}
