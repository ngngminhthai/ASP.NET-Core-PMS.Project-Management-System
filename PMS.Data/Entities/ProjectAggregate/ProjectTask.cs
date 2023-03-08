using PMS.Data.Entities;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Data.Entities.ValueObjects;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class ProjectTask : DomainEntity<int>, IDateTimeStamp
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }

        public int? KanbanColumeID { get; set; }
        [ForeignKey("KanbanColumeID")]
        public KanbanColume? KanbanColume { get; set; }

        public int PriorityValue { get; set; }

        [NotMapped]
        public WorkingStatus WorkingStatus
        {
            get => WorkingStatusValue == 1 ? WorkingStatus.Done :
                   WorkingStatusValue == 2 ? WorkingStatus.InProgress : WorkingStatus;
            set => WorkingStatus = value;
        }
        public int WorkingStatusValue { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<ProjectTask_User> ProjectTask_Users { get; set; }

        [NotMapped]
        public int RemainDate { 
            get {
                

                // Lấy thời gian hiện tại
                DateTime now = DateTime.Now;

                // Tính số ngày còn lại đến ngày kết thúc
                TimeSpan remainingTime = EndDate - now;

                // Lấy số ngày còn lại dưới dạng số nguyên
                int remainingDays = (int)Math.Ceiling(remainingTime.TotalDays);
                return remainingDays;

            } }
    }
}
