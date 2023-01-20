using System;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace WebApplication1.Data.Entities.UserAggregate
{
    public class Notification : DomainEntity<int>
    {
        public ManageUser User { get; set; }
        public DateTime Date { get; set; }
    }
}
