using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.Entities.ValueObjects;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectTask : DomainEntity<int>, IDateTimeStamp
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }

        [NotMapped]
        public Priority Priority
        {
            get => PriorityValue == 1 ? Priority.Low :
                   PriorityValue == 2 ? Priority.Medium : Priority.High;
            set => PriorityValue = value.Level;
        }


        public int PriorityValue { get; set; }

        [NotMapped]
        public WorkingStatus WorkingStatus
        {
            get
            {
                return WorkingStatusValue switch
                {
                    1 => WorkingStatus.Done,
                    2 => WorkingStatus.InProgress,
                    _ => WorkingStatus.NotStarted
                };
            }
            set => WorkingStatusValue = (int)value;
        }

        public int WorkingStatusValue { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        [NotMapped]
        public int Duration
        {
            get
            {
                TimeSpan span = EndDate - StartDate;
                return (int)span.TotalDays;
            }
        }
        [NotMapped]
        public int EarliestStart { get; set; }
        [NotMapped]
        public int EarliestFinish { get; set; }
        [NotMapped]
        public int LatestStart { get; set; }
        [NotMapped]
        public int LatestFinish { get; set; }

        public String Dependencies { get; set; }
        public String Successors { get; set; }

        public List<ProjectTask> DependentTasks { get; set; } = new List<ProjectTask>();
        public List<ProjectTask> SuccessorTaks { get; set; } = new List<ProjectTask>();

        public ICollection<ProjectTask_User> ProjectTask_Users { get; set; }

        public void UpdateDependencies()
        {
            Dependencies = string.Join(",", DependentTasks.Select(t => t.Id));
        }
        public void UpdateSuccessors()
        {
            Successors = string.Join(",", SuccessorTaks.Select(t => t.Id));
        }

        /*       public ProjectTask(string name, DateTime startDate, DateTime endDate)
               {
                   Name = name;
                   StartDate = startDate;
                   EndDate = endDate;
               }
       */
    }
}
