using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Entities;

namespace PMS.Data.Entities.UserAggregate
{
    public class UploadedFiles : DomainEntity<int>, IUpdateTimeStamp
    {
        public string UserUploadedId { get; set; }

        [ForeignKey("UserUploadedId")]
        public ManageUser User { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
