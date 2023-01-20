using PMS.Infrastructure.SharedKernel;
using System;

namespace WebApplication1.Data.Entities.UserAggregate
{
    public class Notification : DomainEntity<int>
    {
        public ManageUser User { get; set; }
        public DateTime Date { get; set; }
    }
}
