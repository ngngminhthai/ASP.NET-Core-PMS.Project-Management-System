using System;

namespace PMS.Infrastructure.Interfaces
{
    public interface IUpdateTimeStamp
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }

}
