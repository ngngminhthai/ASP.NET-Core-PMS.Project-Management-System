using System;

namespace PMS.Infrastructure.Interfaces
{
    public interface IDateTimeStamp
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
